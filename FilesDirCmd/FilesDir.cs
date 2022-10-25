using FilesDir.Flags;
using FilesDir.Utils;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static async Task Main()
    {
        await new Flags().RunFilesDir();
        
        Utils.Tasks.SendNotif("Recherche terminées !", Var.Results.SearchTimer.GetCleenTimer());
        Drawing.End();
    }

    #endregion
}