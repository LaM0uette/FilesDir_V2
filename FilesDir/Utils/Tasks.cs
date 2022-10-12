using System.Reflection;
using FilesDir.Core;

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
    
    //

    #region SEnum

    public static string GetSearchModeReq(this SEnum.SearchMode searchMode)
    {
        return searchMode switch
        {
            SEnum.SearchMode.In => "%",
            SEnum.SearchMode.Equal => "=",
            SEnum.SearchMode.Begin => "^",
            SEnum.SearchMode.End => "$",
            SEnum.SearchMode.Regex => "r",
            _ => "%"
        };
    }

    #endregion
}