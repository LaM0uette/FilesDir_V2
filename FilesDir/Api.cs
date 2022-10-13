using FilesDir.Interfaces;
using FilesDir.Utils;

namespace FilesDir;

public static class Api
{
    public static async Task DirSearchAsync(this IFlags flags, string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetDirectories(sDir), Var.ParallelOptions, async (dir, _) =>
        {
            try
            {
                await flags.DirSearchAsync(dir);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
        });

        await flags.FileSearchAsync(sDir);
    }
    
    public static async Task FileSearchAsync(this IFlags flags, string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetFiles(sDir), Var.ParallelOptions, async (file, _) =>
        {
            try
            {
                await Task.Run(() =>
                {
                    flags.Check(new FileInfo(file));
                }, _);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }
}