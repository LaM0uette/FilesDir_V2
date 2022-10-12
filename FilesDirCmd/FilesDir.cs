using FilesDir;
using FilesDir.Core;
using FilesDir.Utils;
using Logger;

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
        Var.Log.DrawStart(Cst.Logo, Cst.Author, Cst.Version);
    }

    #endregion
}