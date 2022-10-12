﻿using FilesDir;

namespace FilesDirCmd;

public static class FilesDir
{
    #region Statements

    public static void Main()
    {
        var flags = Api.GetFlags();
        
        Console.WriteLine(flags.SearchMode);

        foreach (var word in flags.Words)
        {
            Console.WriteLine(word);
        }
        foreach (var ext in flags.Extensions)
        {
            Console.WriteLine(ext);
        }
        
        Console.WriteLine(flags.PoolSize);
        Console.WriteLine(flags.Casse);
        Console.WriteLine(flags.Utf);
    }

    #endregion
}