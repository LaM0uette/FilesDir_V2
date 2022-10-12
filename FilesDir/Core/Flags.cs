using FilesDir.Interfaces;

namespace FilesDir.Core;

public partial class Flags : IFlags
{
    /// Mode de recherche
    public  MyEnum.SearchMode SearchMode { get; set; }
        
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
        Words = GetWords();
        Extensions = GetExtensions();
        FoldersBlackList = GetFoldersBlackList();
        FoldersWhiteList = GetFoldersWhiteList();
        PoolSize = GetPoolSize();
        Casse = GetCasse();
        Utf = GetUtf();
        Silent = GetSilent();
    }
}