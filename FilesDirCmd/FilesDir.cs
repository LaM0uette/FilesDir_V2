using FilesDir.Flags;
using FilesDir.Interfaces;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static async Task Main()
    {
        await new Flags().RunFilesDir();
    }

    #endregion
}