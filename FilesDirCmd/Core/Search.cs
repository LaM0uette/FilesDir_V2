using System.Diagnostics;
using FilesDir;
using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDirCmd.Core;

public static partial class Search
{
    public static async Task Run(this Structs.SFlags flags)
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Var.Log.Separator("RECHERCHE");

        // TODO: REMETTRE LE BON DOSSIER !
        //await Api.DirSearchAsync(Var.CurrentDir);
        await Api.DirSearchAsync("T:\\- 4 Suivi Appuis\\18-Partage\\de VILLELE DORIAN");
        
        sw.Stop();
        Var.Results.SearchTimer = sw.Elapsed.TotalSeconds;
        
        // TODO: A SUPPRIMER ET A REMPLACER PAR UN BILAN
        Var.Log.VoidPink($"{Var.Results.NbFiles} - {Var.Results.SearchTimer} - {Var.Results.NbFilesTotal} - {Var.Results.NbFolders}");
    }
}