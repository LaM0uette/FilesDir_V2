namespace FilesDir.Core;

public static class Flags
{
    public static SEnum.SearchMode GetFlagSearchMode()
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
    
    public static string[] GetWords()
    {
        return Flaggers.Flags.String("w", "").Split(":");
    }
    
    public static string[] GetExtensions()
    {
        return Flaggers.Flags.String("e", "*").Split(":");
    }

    public static int GetPoolSize()
    {
        return Flaggers.Flags.Int("p", 100);
    }
    
    public static bool GetCasse()
    {
        return Flaggers.Flags.Bool("c", false);
    }
    
    public static bool GetUtf()
    {
        return Flaggers.Flags.Bool("utf", false);
    }
    
    public static bool GetSilent()
    {
        return Flaggers.Flags.Bool("s", false);
    }
    
    public static string[] GetFoldersBlackList()
    {
        return Flaggers.Flags.String("bl", "").Split(":");
    }
    
    public static string[] GetFoldersWhiteList()
    {
        return Flaggers.Flags.String("wl", "").Split(":");
    }
}