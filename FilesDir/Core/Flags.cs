namespace FilesDir.Core;

public static class Flags
{
    #region Statements

    public static Structs.SFlags Get()
    {
        return new Structs.SFlags
        {
            SearchMode = GetFlagSearchMode(),
            Words = GetWords(),
            Extensions = GetExtensions(),
            FoldersBlackList = GetFoldersBlackList(),
            FoldersWhiteList = GetFoldersWhiteList(),
            PoolSize = GetPoolSize(),
            Casse = GetCasse(),
            Utf = GetUtf(),
            Silent = GetSilent()
        };
    }

    #endregion

    //

    #region Fonctions

    private static SEnum.SearchMode GetFlagSearchMode()
    {
        return Flaggers.Flags.String("m", "%") switch
        {
            "%" => SEnum.SearchMode.In,
            "=" => SEnum.SearchMode.Equal,
            "^" => SEnum.SearchMode.Begin,
            "$" => SEnum.SearchMode.End,
            "r" => SEnum.SearchMode.Regex,
            _ => SEnum.SearchMode.In
        };
    }
    
    private static string[] GetWords()
    {
        return Flaggers.Flags.String("w", "").Split(":");
    }
    
    private static string[] GetExtensions()
    {
        return Flaggers.Flags.String("e", "*").Split(":");
    }
    
    private static string[] GetFoldersBlackList()
    {
        return Flaggers.Flags.String("bl", "").Split(":");
    }
    
    private static string[] GetFoldersWhiteList()
    {
        return Flaggers.Flags.String("wl", "").Split(":");
    }

    private static int GetPoolSize()
    {
        return Flaggers.Flags.Int("p", 100);
    }
    
    private static bool GetCasse()
    {
        return Flaggers.Flags.Bool("c", false);
    }
    
    private static bool GetUtf()
    {
        return Flaggers.Flags.Bool("utf", false);
    }
    
    private static bool GetSilent()
    {
        return Flaggers.Flags.Bool("s", false);
    }

    #endregion
}