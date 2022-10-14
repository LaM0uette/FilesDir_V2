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
    public static string ExportsPath { get; } = Path.Join(DirTemp, "exports");

    public static ParallelOptions ParallelOptions = new(){MaxDegreeOfParallelism = 1000};
    
    public static Results Results = new();

    public static List<Exports> Exports = new();

    #endregion
}