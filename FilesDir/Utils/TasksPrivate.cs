using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Tasks
{
    private static bool FolderInFilter(this IFlags flags, string folder)
    {
        flags.FoldersBlackList = Array.ConvertAll(flags.FoldersBlackList, word => word.ToLower());
        flags.FoldersWhiteList = Array.ConvertAll(flags.FoldersWhiteList, word => word.ToLower());

        if (!flags.FoldersBlackList[0].Equals("") && flags.FoldersBlackList.Any(folder.ToLower().Contains))
        {
            return false;
        }
        
        if (!flags.FoldersWhiteList[0].Equals("") && !flags.FoldersWhiteList.Any(folder.ToLower().Contains))
        {
            return false;
        }

        return true;
    }
    
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