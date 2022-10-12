namespace FilesDir.Core;

public static class Structs
{
    public struct SFlags
    {
        /// Mode de recherche
        public SEnum.SearchMode SearchMode;
        
        /// Mot ou liste de mots à rechercher
        public string[] Words;
        
        /// Extension ou liste d'extensions à filtrer
        public string[] Extensions;
        
        /// Taille de processus en simultanés
        public int PoolSize;
        
        /// Sensible à la casse ?
        public bool Casse;
        
        /// Sensible à l'encoding utf-8 ?
        public bool Utf;
        
        /// Mode silencieux, évite les prints à l'écran et latence inutile
        public bool Silent;
        
        /// BlackList des dossiers
        public string[] FoldersBlackList;
        
        /// WhiteList des dossiers (ne cherche que dans ces dossiers)
        public string[] FoldersWhiteList;
    }
}