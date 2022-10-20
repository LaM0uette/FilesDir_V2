using System.Diagnostics;
using FilesDir.Core;
using FilesDir.Interfaces;
using FilesDir.Utils;

namespace FilesDir.Flags;

public partial class Flags : IFlags
{
    #region Statements

    /// Mode de recherche
    public  MyEnum.SearchMode SearchMode { get; set; }
    
    /// Commande regex
    public  string Re { get; set; }
    
    /// Chemin de la recherche
    public  string DirPath { get; set; }
        
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
        SearchMode = Args.GetSearchMode();
        Re = Args.GetRegex();
        DirPath = Args.GetDirPath();
        Words = Args.GetWords();
        Extensions = Args.GetExtensions();
        FoldersBlackList = Args.GetFoldersBlackList();
        FoldersWhiteList = Args.GetFoldersWhiteList();
        PoolSize = Args.GetPoolSize();
        Casse = Args.GetCasse();
        Utf = Args.GetUtf();
        Silent = Args.GetSilent();
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

    public string GetReqOfSearch()
    {
        var req = @"T:\- 11 Outils\FilesDir\FD ";

        req += $"-m={GetSearchModeReq(SearchMode)} ";
        
        if (Words.Length > 0 && !Words[0].Equals("")) req += $"-w={string.Join(":", Words)} ";
        if (Extensions.Length > 0 && !Extensions[0].Equals("")) req += $"-e={string.Join(":", Extensions)} ";
        if (FoldersBlackList.Length > 0 && !FoldersBlackList[0].Equals("")) req += $"-bl={string.Join(":", FoldersBlackList)} ";
        if (FoldersWhiteList.Length > 0 && !FoldersWhiteList[0].Equals("")) req += $"-wl={string.Join(":", FoldersWhiteList)} ";
        
        req += $"-pool={PoolSize} ";

        if (Casse) req += "-c ";
        if (Utf) req += "-utf ";
        if (Silent) req += "-s ";
        
        return req;
    }
    
    public string GetFullReqOfSearch()
    {
        var req = "FilesDir ";

        req += $"-m={GetSearchModeReq(SearchMode)} ";
        req += $"-p=\"{DirPath}\" ";

        if (Words.Length > 0 && !Words[0].Equals("")) req += $"-w={string.Join(":", Words)} ";
        if (Extensions.Length > 0 && !Extensions[0].Equals("")) req += $"-e={string.Join(":", Extensions)} ";
        if (FoldersBlackList.Length > 0 && !FoldersBlackList[0].Equals("")) req += $"-bl={string.Join(":", FoldersBlackList)} ";
        if (FoldersWhiteList.Length > 0 && !FoldersWhiteList[0].Equals("")) req += $"-wl={string.Join(":", FoldersWhiteList)} ";
        
        req += $"-pool={PoolSize} ";

        if (Casse) req += "-c ";
        if (Utf) req += "-utf ";
        if (Silent) req += "-s ";
        
        return req;
    }

    #endregion
    
    //

    #region Actions

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