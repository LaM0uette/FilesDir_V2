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
        };
    }

    #endregion
}