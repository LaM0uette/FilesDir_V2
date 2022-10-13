using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static class Filter
{
    public static string CheckFileCasse(this IFlags flags, string fileName)
    {
        if (flags.Casse) return fileName;

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
}