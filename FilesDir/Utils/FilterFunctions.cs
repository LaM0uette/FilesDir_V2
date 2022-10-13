using System.Text.RegularExpressions;
using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Filter
{
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