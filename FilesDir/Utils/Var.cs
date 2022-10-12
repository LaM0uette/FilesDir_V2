using FilesDir.Core;
using Logger;

namespace FilesDir.Utils;

public static class Var
{
    #region Windows

    public static readonly string DirTemp = Tasks.GetDirTemp();
    
    public static readonly string CurrentDir = Tasks.GetCurrentDir();

    #endregion
    
    //

    #region Configs

    public static Log Log { get; } = new(DirTemp);
    
    public static Dump Dump { get; } = new(DirTemp);
    
    public static ParallelOptions ParallelOptions = new(){MaxDegreeOfParallelism = 1000};

    #endregion

    //

    public static Structs.SResults Results = new();
}