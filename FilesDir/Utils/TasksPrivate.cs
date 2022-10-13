using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Tasks
{
    private static bool FileInFilter(this IFlags flags, FileInfo fi)
    {
        var fileName = fi.Name;

        fileName = flags.CheckFileCasse(fileName);
        fileName = flags.CheckFileEncoding(fileName);

        var fileShortName = fileName.Split(".")[0];
        
        return fileName.CheckFileIsClosed() &&
               flags.CheckSearchMode(fileShortName) &&
               (flags.Extensions.Any("*".Contains) || flags.Extensions.Any(fi.Extension.ToLower().Contains));
    }
}