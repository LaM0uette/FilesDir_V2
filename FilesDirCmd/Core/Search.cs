using System.Diagnostics;
using FilesDir;
using FilesDir.Interfaces;
using FilesDir.Utils;
using Logger;

namespace FilesDirCmd.Core;

public static partial class Search
{
    public static async Task Run(this IFlags flags)
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Var.Log.Separator("RECHERCHE", Log.TypeLog.Cmd);

        // TODO: REMETTRE LE BON DOSSIER !
        //await Api.DirSearchAsync(Var.CurrentDir);
        await flags.DirSearchAsync("T:\\- 4 Suivi Appuis\\18-Partage\\de VILLELE DORIAN");

        sw.Stop();
        Var.Results.SearchTimer = sw.Elapsed.TotalSeconds;
    }
}