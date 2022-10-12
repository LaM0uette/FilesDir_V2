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

        return req;
    }
}