using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDirCmd.Utils;

public static class Tasks
{
    public static string GetReqOfSearch(this Structs.SFlags flags)
    {
        var req = "FilesDir ";

        req += $"m={flags.SearchMode.GetSearchModeReq()} ";
        req += $"w={string.Join(":", flags.Words)} ";
        req += $"e={string.Join(":", flags.Extensions)} ";
        req += $"p={flags.PoolSize} ";
        req += $"bl={string.Join(":", flags.FoldersBlackList)} ";
        req += $"wl={string.Join(":", flags.FoldersWhiteList)} ";
        
        if (flags.Casse) req += "-c";
        if (flags.Utf) req += "-utf";
        if (flags.Silent) req += "-s";
        
        return req;
    }
}