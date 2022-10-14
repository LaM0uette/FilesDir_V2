using System.Diagnostics;
using FilesDir.Core;
using FilesDir.Interfaces;
using FilesDir.Utils;
using FilesDirCmd.Core;
using FilesDirCmd.Utils;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static async Task Main()
    {
        await new Flags().RunFilesDir();
    }

    public static async Task WpfMain(this IFlags flags)
    {
        await flags.RunFilesDir();
    }

    #endregion
    
    //

    #region Fonctions

    private static async Task RunFilesDir(this IFlags flags)
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

        Tasks.GenerateExcel();
        
        flags.Bilan();
    }

    #endregion
}