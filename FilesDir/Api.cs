using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDir;

public static class Api
{
    #region Functions

    public static Structs.SFlags GetFlags()
    {
        return new Structs.SFlags
        {
            SearchMode = Flags.GetFlagSearchMode(),
            Words = Flags.GetWords(),
            Extensions = Flags.GetExtensions(),
            FoldersBlackList = Flags.GetFoldersBlackList(),
            FoldersWhiteList = Flags.GetFoldersWhiteList(),
            PoolSize = Flags.GetPoolSize(),
            Casse = Flags.GetCasse(),
            Utf = Flags.GetUtf(),
            Silent = Flags.GetSilent()
        };
    }
    
    public static async Task DirSearchAsync(string sDir)
    {
        // For each folders
        await Parallel.ForEachAsync(Directory.GetDirectories(sDir), Var.ParallelOptions, async (dir, _) =>
        {
            try
            {
                await DirSearchAsync(dir);
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
            catch (Exception)
            {
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
        });
        
        // For each files
        await Parallel.ForEachAsync(Directory.GetFiles(sDir), Var.ParallelOptions, async (file, _) =>
        {
            try
            {
                await Task.Run(() =>
                {
                    var fileInfo = new FileInfo(file);
                    var creaDate = File.GetCreationTime(file);
                    var modifDate = File.GetLastWriteTime(file);

                    if (!fileInfo.Name.ToLower().Contains("cem") || file.Contains('~')) return;

                    Interlocked.Add(ref Var.Results.NbFiles, 1);
                    
                    Var.Log.Ok(fileInfo.Name);
                    Var.Dump.String($"{Var.Results.NbFiles};{fileInfo.Name};{creaDate};{modifDate};{fileInfo.DirectoryName};{fileInfo.Directory}");
                }, _);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }

    #endregion
}