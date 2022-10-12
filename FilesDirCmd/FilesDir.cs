using System.Diagnostics;
using FilesDir;
using FilesDir.Core;
using FilesDirCmd.Core;
using FilesDirCmd.Utils;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static void Main()
    {
        var flags = Api.GetFlags();
        flags.RunFilesDir();
    }

    public static void WpfMain(this Structs.SFlags flags)
    {
        flags.RunFilesDir();
    }

    #endregion
    
    //

    #region Fonctions

    private static void RunFilesDir(this Structs.SFlags flags)
    {
        Drawing.Start();
        var totalTimer = new Stopwatch();
        totalTimer.Start();
        
        flags.Init();
        flags.CheckPoolSize();
        
        flags.Run();

        totalTimer.Stop();
        //Drawing.End();
    }

    #endregion
}