using System.Text.RegularExpressions;
using FilesDir.Core;
using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static class Filter
{
    public static string CheckFileCasse(this IFlags flags, string fileName)
    {
        if (flags.Casse || flags.SearchMode.Equals(MyEnum.SearchMode.Regex)) return fileName;

        flags.Words = Array.ConvertAll(flags.Words, word => word.ToLower());
        return fileName.ToLower();
    }
    
    public static string CheckFileEncoding(this IFlags flags, string fileName)
    {
        if (flags.Utf) return fileName;

        flags.Words = Array.ConvertAll(flags.Words, word => word.RemoveAccent());
        return fileName.RemoveAccent();
    }
    
    public static bool CheckFileIsClosed(this string fileName)
    {
        return !fileName.Contains('~');
    }
    
    public static bool CheckSearchMode(this IFlags flags, string fileName)
    {
        return flags.SearchMode switch
        {
            MyEnum.SearchMode.In => flags.CheckSearchModeIn(fileName),
            MyEnum.SearchMode.Equal => flags.CheckSearchModeEqual(fileName),
            MyEnum.SearchMode.Begin => flags.CheckSearchModeBegin(fileName),
            MyEnum.SearchMode.End => flags.CheckSearchModeEnd(fileName),
            MyEnum.SearchMode.Regex => flags.CheckSearchModeRegex(fileName),
            _ => flags.CheckSearchModeIn(fileName)
        };
    }
    
    
    
    //
    
    
    
    private static bool CheckSearchModeIn(this IFlags flags, string fileName)
    {
        return flags.Words.Any(fileName.Contains);
    }
    
    private static bool CheckSearchModeEqual(this IFlags flags, string fileName)
    {
        return flags.Words.Any(fileName.Equals);
    }
    
    private static bool CheckSearchModeBegin(this IFlags flags, string fileName)
    {
        return flags.Words.Any(fileName.StartsWith);
    }
    
    private static bool CheckSearchModeEnd(this IFlags flags, string fileName)
    {
        return flags.Words.Any(fileName.EndsWith);
    }
    
    private static bool CheckSearchModeRegex(this IFlags flags, string fileName)
    {
        return Regex.Match(fileName, flags.Regex).Success;
    }
}