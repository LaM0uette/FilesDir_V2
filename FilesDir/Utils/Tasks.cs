﻿using System.Reflection;
using CommonTasks;
using FilesDir.Core;

namespace FilesDir.Utils;

public static class Tasks
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

    public static string GetFileName(this string dir)
    {
        return Path.GetFileName(dir);
    }
    
    public static string GetFilePath(this string dir)
    {
        return Path.GetDirectoryName(dir) ?? string.Empty;
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
    
    //

    #region Structs

    public static string GetReqOfSearch(this Structs.SFlags flags)
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

    #endregion
}