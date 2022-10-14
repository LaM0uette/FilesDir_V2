using FilesDir.Core;
using FilesDir.Interfaces;

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
}