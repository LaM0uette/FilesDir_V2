using System.Diagnostics;
using FilesDir;
using FilesDir.Core;
using FilesDir.Utils;
using FilesDirCmd.Core;
using FilesDirCmd.Utils;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static async Task Main()
    {
        await Flags.Get().RunFilesDir();
    }

    public static async Task WpfMain(this Structs.SFlags flags)
    {
        await flags.RunFilesDir();
    }

    #endregion
    
    //

    #region Fonctions

    private static async Task RunFilesDir(this Structs.SFlags flags)
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Drawing.Start();

        flags.Init();
        flags.CheckPoolSize();
        flags.SetPoolSize();

        await flags.Run();

        sw.Stop();
        Var.Results.TotalTimer = sw.Elapsed.TotalSeconds;
        
        // TODO: A SUPPRIMER ET A REMPLACER PAR UN BILAN
        Var.Log.Separator("BILAN");
        Var.Log.VoidPink($"{Var.Results.NbFiles} - {Var.Results.SearchTimer} - {Var.Results.NbFilesTotal} - {Var.Results.NbFolders}");
        
        Drawing.End();
    }

    #endregion
}