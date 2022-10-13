﻿using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Tasks
{
    private static bool FileInFilter(this IFlags flags, FileInfo fi)
    {
        var fileName = fi.Name;

        fileName = flags.CheckFileCasse(fileName);
        fileName = flags.CheckFileEncoding(fileName);

        if (
            !fileName.CheckFileIsClosed() ||
            !flags.Words.Any(fileName.Contains)
            ) 
            return false;

        return true;
    }
}