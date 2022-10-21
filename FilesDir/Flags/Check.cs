using System.Text.RegularExpressions;
using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDir.Flags;

public partial class Flags
{
    private bool CheckFolder(string folder)
    {
        return FolderInFilter(folder);
    }

    private void CheckFile(FileInfo fi)
    {
        Var.Log.ProgressInfini("Dossiers: ", Var.Results.NbFolders, " || Fichiers traités: ", Var.Results.NbFilesTotal, " || Fichiers trouvés: ", Var.Results.NbFiles);

        if (!FileInFilter(fi)) return;

        Interlocked.Add(ref Var.Results.NbFiles, 1);

#pragma warning disable CA1416
        var prop = fi.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount))!.ToString();
#pragma warning restore CA1416

        Var.Log.OkDel($"N°{Var.Results.NbFiles} => {fi.Name}");
        Var.Dump.String($"{Var.Results.NbFiles};{fi.Name};{fi.Extension.Split(".")[1]};{fi.CreationTime};{fi.LastWriteTime};{fi.LastAccessTime};{fi.FullName};{fi.Directory};{prop}");
        
        Var.Exports.Add(new Exports
        {
            Id = Var.Results.NbFiles,
            Name = fi.Name,
            Extension = fi.Extension.Split(".")[1],
            CreaDate = fi.CreationTime,
            ModifDate = fi.LastWriteTime,
            AccesDate = fi.LastAccessTime,
            FullName = fi.FullName,
            Path = $"{fi.Directory}",
            Proprietaire = prop
        });
    }
    
    private bool FolderInFilter(string folder)
    {
        FoldersBlackList = Array.ConvertAll(FoldersBlackList, word => word.ToLower());
        FoldersWhiteList = Array.ConvertAll(FoldersWhiteList, word => word.ToLower());

        if (!FoldersBlackList[0].Equals("") && FoldersBlackList.Any(folder.ToLower().Contains))
        {
            return false;
        }
        
        if (!FoldersWhiteList[0].Equals("") && !FoldersWhiteList.Any(folder.ToLower().Contains))
        {
            return false;
        }

        return true;
    }
    
    private bool FileInFilter(FileInfo fi)
    {
        var fileName = fi.Name;

        fileName = CheckFileCasse(fileName);
        fileName = CheckFileEncoding(fileName);

        var fileShortName = fileName.Split(".")[0];
        
        return CheckFileIsClosed(fileName) &&
               CheckSearchMode(fileShortName) &&
               (Extensions.Any("*".Contains) || Extensions.Any(fi.Extension.ToLower().Contains));
    }
    
    private string CheckFileCasse(string fileName)
    {
        if (Casse || SearchMode.Equals(MyEnum.SearchMode.Re)) return fileName;

        Words = Array.ConvertAll(Words, word => word.ToLower());
        return fileName.ToLower();
    }
    
    private string CheckFileEncoding(string fileName)
    {
        if (Utf) return fileName;

        Words = Array.ConvertAll(Words, word => word.RemoveAccent());
        return fileName.RemoveAccent();
    }
    
    private static bool CheckFileIsClosed(string fileName)
    {
        return !fileName.Contains('~');
    }
    
    private bool CheckSearchMode(string fileName)
    {
        return SearchMode switch
        {
            MyEnum.SearchMode.In => Words.Any(fileName.Contains),
            MyEnum.SearchMode.Equal => Words.Any(fileName.Equals),
            MyEnum.SearchMode.Begin => Words.Any(fileName.StartsWith),
            MyEnum.SearchMode.End => Words.Any(fileName.EndsWith),
            MyEnum.SearchMode.Re => Regex.Match(fileName, Re).Success,
            _ => Words.Any(fileName.Contains)
        };
    }
}