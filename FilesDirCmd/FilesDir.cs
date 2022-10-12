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
        var flags = Api.GetFlags();
        await flags.RunFilesDir();
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
        Drawing.Start();
        var sw = new Stopwatch();
        sw.Start();
        
        flags.Init();
        flags.CheckPoolSize();
        
        await flags.Run();

        sw.Stop();
        Var.Results.TotalTimer = sw.Elapsed.TotalSeconds;
        
        //Drawing.End();
    }

    #endregion
}