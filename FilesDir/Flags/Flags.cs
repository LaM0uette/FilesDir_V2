using System.Diagnostics;
using System.Text.RegularExpressions;
using FilesDir.Core;
using FilesDir.Interfaces;
using FilesDir.Utils;
using FilesDirCmd.Utils;
using Logger;

namespace FilesDir.Flags;

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
        SearchMode = Args.GetSearchMode();
        Re = Args.GetRegex();
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