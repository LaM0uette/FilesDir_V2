using FilesDir.Core;

namespace FilesDir;

public static class Api
{
    #region Functions

    public static Structs.SFlags GetFlags()
    {
        return new Structs.SFlags
        {
            SearchMode = Flags.GetFlagSearchMode(),
            Words = Flags.GetWords(),
            Extensions = Flags.GetExtensions(),
            PoolSize = Flags.GetPoolSize(),
            Casse = Flags.GetCasse(),
            Utf = Flags.GetUtf(),
            Silent = Flags.GetSilent(),
            FoldersBlackList = Flags.GetFoldersBlackList(),
            FoldersWhiteList = Flags.GetFoldersWhiteList(),
        };
    }

    #endregion
}