using System.Diagnostics;
using FilesDir.Utils;
using Logger;

namespace FilesDir.Flags;

public partial class Flags
{
    private async Task RunSearch()
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Var.Log.Separator("RECHERCHE", Log.TypeLog.Cmd);

        await DirSearchAsync(DirPath);

        sw.Stop();
        Var.Results.SearchTimer = TimeSpan.FromSeconds(sw.Elapsed.TotalSeconds);
    }

    private async Task DirSearchAsync(string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetDirectories(sDir), Var.ParallelOptions, async (dir, _) =>
        {
            try
            {
                await DirSearchAsync(dir);
            }
            catch (Exception)
            {
                Var.Log.NokDel($"Impossible d'accéder au dossier : {sDir}");
                Interlocked.Add(ref Var.Results.NbErrFolders, 1);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFolders, 1);
            }
        });

        if (!CheckFolder(sDir)) return;
        
        await FileSearchAsync(sDir);
    }
    
    private async Task FileSearchAsync(string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetFiles(sDir), Var.ParallelOptions, async (file, _) =>
        {
            try
            {
                await Task.Run(() =>
                {
                    CheckFile(new FileInfo(file));
                }, _);
            }
            catch (Exception)
            {
                Var.Log.NokDel($"Impossible d'accéder au fichier : {file}");
                Interlocked.Add(ref Var.Results.NbErrFiles, 1);
            }
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }
}