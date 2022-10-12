using FilesDir.Core;
using Logger;

namespace FilesDir.Utils;

public static class Var
{
    #region Windows

    public static readonly string DirTemp = Tasks.GetDirTemp();

    #endregion
    
    //
    
    public static Log Log { get; } = new(DirTemp);

    public static Structs.SResults Results = new();
}