using System.Diagnostics;
using FilesDir;
using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDirCmd.Core;

public static class Search
{
    #region Fonctions
    
    public static void Init(this Structs.SFlags flags)
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.Param("INITIALISATION DE LA RECHERCHE EN COURS...");

        Var.Log.Param("CREATION DE LA REQUETE EN COURS...");
        Var.Results.Req = flags.GetReqOfSearch();
        
        Var.Log.Param("REQUETE UTILISEE : ", Var.Results.Req);
    }

    public static void CheckPoolSize(this Structs.SFlags flags)
    {
        if (flags.PoolSize < 2)
        {
            flags.PoolSize = 2;
            Var.Log.Nok("Poolsize trop petite, impossible de mettre moins de 2");
            Var.Log.Info("Modification de la Poolsize en cours");
        }
        
        Var.Log.Param("POOLSIZE MISE A : ", flags.PoolSize.ToString());
    }

    public static async Task Run(this Structs.SFlags flags)
    {
        Var.Log.Separator("RECHERCHE");
        
        var sw = new Stopwatch();
        sw.Start();
        
        await Api.DirSearchAsync("T:\\- 4 Suivi Appuis\\18-Partage");
        
        sw.Stop();
        Var.Results.SearchTimer = sw.Elapsed.TotalSeconds;
        
        Var.Log.Ok($"{Var.Results.NbFiles} - {Var.Results.SearchTimer} - {Var.Results.NbFilesTotal} - {Var.Results.NbFolders}");
    }

    #endregion
}