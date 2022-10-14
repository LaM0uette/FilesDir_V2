using System.Diagnostics;
using System.Text.RegularExpressions;
using FilesDir.Interfaces;
using FilesDir.Utils;
using FilesDirCmd.Utils;
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
    
    public async Task RunFilesDir()
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Drawing.Start();

        Init();
        CheckPoolSize();
        SetPoolSize();

        await RunSearch();

        sw.Stop();
        Var.Results.TotalTimer = sw.Elapsed.TotalSeconds;

        Tasks.GenerateExcel();
        
        this.Bilan();
    }

    private async Task RunSearch()
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

    public string GetReqOfSearch()
    {
        var req = "FilesDir ";

        req += $"m={GetSearchModeReq(SearchMode)} ";
        req += $"w={string.Join(":", Words)} ";
        req += $"e={string.Join(":", Extensions)} ";
        req += $"p={PoolSize} ";
        
        if (!FoldersBlackList[0].Equals("")) req += $"bl={string.Join(":", FoldersBlackList)} ";
        if (!FoldersWhiteList[0].Equals("")) req += $"wl={string.Join(":", FoldersWhiteList)} ";
        
        if (Casse) req += "-c ";
        if (Utf) req += "-utf ";
        if (Silent) req += "-s ";
        
        return req;
    }

    #endregion
    
    //

    #region Actions
    
    private void Init()
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.Param("INITIALISATION DE LA RECHERCHE EN COURS...");

        Var.Log.Param("CREATION DE LA REQUETE EN COURS...");
        Var.Results.Req = GetReqOfSearch();
        
        Var.Log.Param("REQUETE UTILISEE : ", Var.Results.Req);
        
        Var.Log.Param("INITIALISATION DU FICHIER DUMP...");
        Var.Dump.String("Id;Fichier;Date creation;Date de modification;Lien du fichier;Lien du dossier");
        
        Var.Log.Param("CREATION DU DOSSIER D'EXPORT...");
        Tasks.CreateDir(Path.Join(Var.DirTemp, "exports"));
    }

    private void CheckPoolSize()
    {
        if (PoolSize < 2)
        {
            Var.Log.Nok("Poolsize trop petite, impossible de mettre moins de 2");
            Var.Log.Param("Modification de la Poolsize en cours...");
            PoolSize = 2;
        }
        
        Var.Log.Param("POOLSIZE MISE A : ", PoolSize.ToString());
    }

    private void SetPoolSize()
    {
        Var.ParallelOptions = new ParallelOptions {MaxDegreeOfParallelism = PoolSize};
        Var.Log.Param("CONFIGURATION DE LA POOLSIZE : ", "OK");
    }
    
    //

    private async Task DirSearchAsync(string sDir)
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

        if (!CheckFolder(sDir)) return;
        
        await FileSearchAsync(sDir);
    }
    
    private async Task FileSearchAsync(string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetFiles(sDir), Var.ParallelOptions, async (file, _) =>
        {
            try
            {
                await Task.Run(() =>
                {
                    CheckFile(new FileInfo(file));
                }, _);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }
    
    private bool CheckFolder(string folder)
    {
        return FolderInFilter(folder);
    }

    private void CheckFile(FileInfo fi)
    {
        // TODO: A optimiser tout les 500msg avec le mode silencieux
        if (!Words[0].Equals(""))
            Var.Log.ProgressInfini("Dossiers: ", Var.Results.NbFolders, " || Fichiers traités: ", Var.Results.NbFilesTotal, " || Fichiers trouvés: ", Var.Results.NbFiles);

        if (!FileInFilter(fi)) return;

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
    
    private static string GetSearchModeReq(MyEnum.SearchMode searchMode)
    {
        return searchMode switch
        {
            MyEnum.SearchMode.In => "%",
            MyEnum.SearchMode.Equal => "=",
            MyEnum.SearchMode.Begin => "^",
            MyEnum.SearchMode.End => "$",
            MyEnum.SearchMode.Re => "r",
            _ => "%"
        };
    }

    #endregion
}