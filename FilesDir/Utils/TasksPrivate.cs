namespace FilesDir.Utils;

public static partial class Tasks
{
    private static bool FileInFilter(this FileInfo fi)
    {
        if (!fi.Name.ToLower().Contains("cem") || fi.Name.Contains('~')) return false;
        
        return true;
    }
}