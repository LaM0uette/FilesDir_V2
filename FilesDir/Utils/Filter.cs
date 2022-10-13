using FilesDir.Core;
using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Filter
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
}