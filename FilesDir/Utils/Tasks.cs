using System.Reflection;
using CommonTasks;
using FilesDir.Core;
using FilesDir.Interfaces;

namespace FilesDir.Utils;

public static partial class Tasks
{
    #region Windows
    
    public static string GetCurrentDir()
    {
        return Directory.GetCurrentDirectory();
    }
    
    public static string GetCurrentVersion()
    {
        var assVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
        return assVersion.Remove(assVersion.Length - 2);
    }
    
    public static string GetDirTemp()
    {
        return $"C:\\Users\\{TskWindows.GetGuid()}\\FilesDIR_Temp";
    }

    #endregion
    
    //

    #region Functions

    public static string GetSearchModeReq(this MyEnum.SearchMode searchMode)
    {
        return searchMode switch
        {
            MyEnum.SearchMode.In => "%",
            MyEnum.SearchMode.Equal => "=",
            MyEnum.SearchMode.Begin => "^",
            MyEnum.SearchMode.End => "$",
            MyEnum.SearchMode.Regex => "r",
            _ => "%"
        };
    }

    public static string GetReqOfSearch(this IFlags flags)
    {
        var req = "FilesDir ";

        req += $"m={flags.SearchMode.GetSearchModeReq()} ";
        req += $"w={string.Join(":", flags.Words)} ";
        req += $"e={string.Join(":", flags.Extensions)} ";
        req += $"p={flags.PoolSize} ";
        req += $"bl={string.Join(":", flags.FoldersBlackList)} ";
        req += $"wl={string.Join(":", flags.FoldersWhiteList)} ";
        
        if (flags.Casse) req += "-c ";
        if (flags.Utf) req += "-utf ";
        if (flags.Silent) req += "-s ";
        
        return req;
    }

    public static void Check(this FileInfo fi)
    {
        if (!fi.FileInFilter()) return;

        Interlocked.Add(ref Var.Results.NbFiles, 1);
                    
        Var.Log.Ok(fi.Name);
        Var.Dump.String($"{Var.Results.NbFiles};{fi.Name};{fi.CreationTime};{fi.LastWriteTime};{fi.FullName};{fi.Directory}");
    }

    #endregion
}