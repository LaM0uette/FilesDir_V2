using System.Reflection;

namespace FilesDir.Utils;

public static class Tasks
{
    #region Windows
    
    public static string GetDirTemp()
    {
        return Path.GetTempPath();
    }
    
    public static string GetCurrentVersion()
    {
        var assVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
        return assVersion.Remove(assVersion.Length - 2);
    }

    #endregion
}