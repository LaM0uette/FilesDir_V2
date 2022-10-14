using FilesDir.Core;

namespace FilesDir.Flags;

public static class Args
{
    public static MyEnum.SearchMode GetSearchMode()
    {
        return Flaggers.Flags.String("m", "%") switch
        {
            "%" => MyEnum.SearchMode.In,
            "=" => MyEnum.SearchMode.Equal,
            "^" => MyEnum.SearchMode.Begin,
            "$" => MyEnum.SearchMode.End,
            "r" => MyEnum.SearchMode.Re,
            _ => MyEnum.SearchMode.In
        };
    }
    
    public static string GetRegex()
    {
        return Flaggers.Flags.String("re", "");
    }
    
    public static string[] GetWords()
    {
        return Flaggers.Flags.String("w", "").Split(":");
    }
    
    public static string[] GetExtensions()
    {
        return Flaggers.Flags.String("e", "*").ToLower().Split(":");
    }
    
    public static string[] GetFoldersBlackList()
    {
        return Flaggers.Flags.String("bl", "").Split(":");
    }
    
    public static string[] GetFoldersWhiteList()
    {
        return Flaggers.Flags.String("wl", "").Split(":");
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
}