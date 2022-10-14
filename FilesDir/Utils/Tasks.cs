using System.Globalization;
using System.Reflection;
using System.Text;
using CommonTasks;
using FilesDir.Core;
using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Tasks
{
    #region Windows
    
    public static string GetCurrentDir()
    {
        return Directory.GetCurrentDirectory();
    }
    
    public static string GetCurrentVersion()
    {
        var assVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
        return assVersion.Remove(assVersion.Length - 2);
    }
    
    public static string GetDirTemp()
    {
        return $"C:\\Users\\{TskWindows.GetGuid()}\\FilesDIR_Temp";
    }

    #endregion
    
    //

    #region Functions

    public static string GetSearchModeReq(this MyEnum.SearchMode searchMode)
    {
        return searchMode switch
        {
            MyEnum.SearchMode.In => "%",
            MyEnum.SearchMode.Equal => "=",
            MyEnum.SearchMode.Begin => "^",
            MyEnum.SearchMode.End => "$",
            MyEnum.SearchMode.Regex => "r",
            _ => "%"
        };
    }

    public static string GetReqOfSearch(this IFlags flags)
    {
        var req = "FilesDir ";

        req += $"m={flags.SearchMode.GetSearchModeReq()} ";
        req += $"w={string.Join(":", flags.Words)} ";
        req += $"e={string.Join(":", flags.Extensions)} ";
        req += $"p={flags.PoolSize} ";
        
        if (!flags.FoldersBlackList[0].Equals("")) req += $"bl={string.Join(":", flags.FoldersBlackList)} ";
        if (!flags.FoldersWhiteList[0].Equals("")) req += $"wl={string.Join(":", flags.FoldersWhiteList)} ";
        
        if (flags.Casse) req += "-c ";
        if (flags.Utf) req += "-utf ";
        if (flags.Silent) req += "-s ";
        
        return req;
    }

    public static bool CheckFolder(this IFlags flags, string folder)
    {
        return flags.FolderInFilter(folder);
    }

    public static void CheckFile(this IFlags flags, FileInfo fi)
    {
        // TODO: A optimiser tout les 500msg avec le mode silencieux
        if (!flags.Words[0].Equals(""))
            Var.Log.ProgressInfini("Dossiers: ", Var.Results.NbFolders, " || Fichiers traités: ", Var.Results.NbFilesTotal, " || Fichiers trouvés: ", Var.Results.NbFiles);

        if (!flags.FileInFilter(fi)) return;

        Interlocked.Add(ref Var.Results.NbFiles, 1);

        Var.Log.OkDel($"N°{Var.Results.NbFiles} => {fi.Name}");
        Var.Dump.String($"{Var.Results.NbFiles};{fi.Name};{fi.CreationTime};{fi.LastWriteTime};{fi.FullName};{fi.Directory}");
        
        Var.Exports.Add(new Exports
        {
            Id = Var.Results.NbFiles,
            Name = fi.Name,
            CreaDate = fi.CreationTime,
            ModifDate = fi.LastWriteTime,
            FullName = fi.FullName,
            Path = $"{fi.Directory}"
        });
    }
    
    public static string RemoveAccent(this string txt)
    {
        var normalizedString = txt.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static void ConvertCsvToXlsx()
    {
        var csv = @$"{Var.DirTemp}\dumps\{Var.Dump.FileName}.csv";
        var xls = @$"{Var.DirTemp}\{Var.ExportsPath}\{Var.Dump.FileName.Replace("Dump", "Export")}.xlsx";
        
        
    }

    #endregion
}