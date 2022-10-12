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
        return Flaggers.Flags.String("e", "").Split(":");
    }
}