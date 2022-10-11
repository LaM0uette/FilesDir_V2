using FilesDir_V2.Core;

namespace FilesDir_V2;

public static class FilesDir
{
    #region Statements

    public static void Main()
    {
        var flags = GetFlags();
    }

    #endregion

    //

    #region Functions

    private static Structs.SFlags GetFlags()
    {
        return new Structs.SFlags
        {
            SearchMode = Flags.GetFlagSearchMode(),
            Words = Flags.GetWords(),
        };
    }

    #endregion
}

