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

        // TODO: REMETTRE LE BON DOSSIER !
        await DirSearchAsync(Var.CurrentDir);
        //await DirSearchAsync("T:\\- 4 Suivi Appuis\\18-Partage\\de VILLELE DORIAN");

        sw.Stop();
        Var.Results.SearchTimer = sw.Elapsed.TotalSeconds;
    }

    private async Task DirSearchAsync(string sDir)
    {
        await Parallel.ForEachAsync(Directory.GetDirectories(sDir), Var.ParallelOptions, async (dir, _) =>
        {
            try
            {
                await DirSearchAsync(dir);
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
            finally
            {
                Interlocked.Add(ref Var.Results.NbFilesTotal, 1);
            }
        });
    }
}