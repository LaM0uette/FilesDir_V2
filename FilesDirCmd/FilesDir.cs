using FilesDir.Flags;
using FilesDir.Interfaces;
using FilesDir.Utils;
using Microsoft.Toolkit.Uwp.Notifications;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static async Task Main()
    {
        await new Flags().RunFilesDir();

        new ToastContentBuilder()
            .AddText("Recherche terminées !")
            .AddText($"{Var.Results.NbFiles} fichiers trouvés en {Var.Results.SearchTimer}s")
            .SetToastDuration(ToastDuration.Short)
            .Show();
        
        Drawing.End();
    }

    #endregion
}