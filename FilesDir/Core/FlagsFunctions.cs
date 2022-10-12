namespace FilesDir.Core;

public partial class Flags
{
    private static MyEnum.SearchMode GetFlagSearchMode()
    {
        return Flaggers.Flags.String("m", "%") switch
        {
            "%" => MyEnum.SearchMode.In,
            "=" => MyEnum.SearchMode.Equal,
            "^" => MyEnum.SearchMode.Begin,
            "$" => MyEnum.SearchMode.End,
            "r" => MyEnum.SearchMode.Regex,
            _ => MyEnum.SearchMode.In
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
}