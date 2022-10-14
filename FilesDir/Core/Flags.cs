using System.Diagnostics;
using System.Text.RegularExpressions;
using FilesDir.Interfaces;
using FilesDir.Utils;
using Logger;

namespace FilesDir.Core;

public partial class Flags : IFlags
{
    #region Statements

    /// Mode de recherche
    public  MyEnum.SearchMode SearchMode { get; set; }
    
    /// Commande regex
    public  string Re { get; set; }
        
    /// Mot ou liste de mots à rechercher
    public string[] Words { get; set; }
        
    /// Extension ou liste d'extensions à filtrer
    public string[] Extensions { get; set; }
        
    /// BlackList des dossiers
    public string[] FoldersBlackList { get; set; }
        
    /// WhiteList des dossiers (ne cherche que dans ces dossiers)
    public string[] FoldersWhiteList { get; set; }
        
    /// Taille de processus en simultanés
    public int PoolSize { get; set; }
        
    /// Sensible à la casse ?
    public bool Casse { get; set; }
        
    /// Sensible à l'encoding utf-8 ?
    public bool Utf { get; set; }
        
    /// Mode silencieux, évite les prints à l'écran et latence inutile
    public bool Silent { get; set; }

    /// <summary>
    /// Initialisation du constructeur
    /// </summary>
    public Flags()
    {
        SearchMode = GetFlagSearchMode();
        Re = GetFlagRegex();
        Words = GetWords();
        Extensions = GetExtensions();
        FoldersBlackList = GetFoldersBlackList();
        FoldersWhiteList = GetFoldersWhiteList();
        PoolSize = GetPoolSize();
        Casse = GetCasse();
        Utf = GetUtf();
        Silent = GetSilent();
    }

    #endregion

    //

    #region Fonctions

    public async Task Run()
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Var.Log.Separator("RECHERCHE", Log.TypeLog.Cmd);

        // TODO: REMETTRE LE BON DOSSIER !
        //await Api.DirSearchAsync(Var.CurrentDir);
        await DirSearchAsync("T:\\- 4 Suivi Appuis\\18-Partage\\de VILLELE DORIAN");

        sw.Stop();
        Var.Results.SearchTimer = sw.Elapsed.TotalSeconds;
    }
    
    public async Task DirSearchAsync(string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetDirectories(sDir), Var.ParallelOptions, async (dir, _) =>
        {
            try
            {
                await DirSearchAsync(dir);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
        });

        if (!this.CheckFolder(sDir)) return;
        
        await FileSearchAsync(sDir);
    }
    
    public async Task FileSearchAsync(string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetFiles(sDir), Var.ParallelOptions, async (file, _) =>
        {
            try
            {
                await Task.Run(() =>
                {
                    this.CheckFile(new FileInfo(file));
                }, _);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }
    
    public bool CheckFolder(string folder)
    {
        return this.FolderInFilter(folder);
    }

    public void CheckFile(FileInfo fi)
    {
        // TODO: A optimiser tout les 500msg avec le mode silencieux
        if (!Words[0].Equals(""))
            Var.Log.ProgressInfini("Dossiers: ", Var.Results.NbFolders, " || Fichiers traités: ", Var.Results.NbFilesTotal, " || Fichiers trouvés: ", Var.Results.NbFiles);

        if (!this.FileInFilter(fi)) return;

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
    
    public bool FolderInFilter(string folder)
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
    
    public bool FileInFilter(FileInfo fi)
    {
        var fileName = fi.Name;

        fileName = this.CheckFileCasse(fileName);
        fileName = this.CheckFileEncoding(fileName);

        var fileShortName = fileName.Split(".")[0];
        
        return CheckFileIsClosed(fileName) &&
               this.CheckSearchMode(fileShortName) &&
               (this.Extensions.Any("*".Contains) || this.Extensions.Any(fi.Extension.ToLower().Contains));
    }
    
    public string CheckFileCasse(string fileName)
    {
        if (Casse || SearchMode.Equals(MyEnum.SearchMode.Re)) return fileName;

        Words = Array.ConvertAll(Words, word => word.ToLower());
        return fileName.ToLower();
    }
    
    public string CheckFileEncoding(string fileName)
    {
        if (Utf) return fileName;

        Words = Array.ConvertAll(Words, word => word.RemoveAccent());
        return fileName.RemoveAccent();
    }
    
    public bool CheckFileIsClosed(string fileName)
    {
        return !fileName.Contains('~');
    }
    
    public bool CheckSearchMode(string fileName)
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

    #endregion
}