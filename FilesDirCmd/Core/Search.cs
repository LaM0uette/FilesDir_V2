using System.Diagnostics;
using FilesDir;
using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDirCmd.Core;

public static class Search
{
    #region Configurations

    public static void Init(this Structs.SFlags flags)
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.Param("INITIALISATION DE LA RECHERCHE EN COURS...");

        Var.Log.Param("CREATION DE LA REQUETE EN COURS...");
        Var.Results.Req = flags.GetReqOfSearch();
        
        Var.Log.Param("REQUETE UTILISEE : ", Var.Results.Req);
        
        Var.Log.Param("INITIALISATION DU FICHIER DUMP...");
        Var.Dump.String("Id;Fichier;Date creation;Date de modification;Lien du fichier;Lien du dossier");
    }

    public static void CheckPoolSize(this Structs.SFlags flags)
    {
        if (flags.PoolSize < 2)
        {
            Var.Log.Nok("Poolsize trop petite, impossible de mettre moins de 2");
            Var.Log.Param("Modification de la Poolsize en cours...");
            flags.PoolSize = 2;
        }
        
        Var.Log.Param("POOLSIZE MISE A : ", flags.PoolSize.ToString());
    }

    public static void SetPoolSize(this Structs.SFlags flags)
    {
        Var.ParallelOptions = new ParallelOptions {MaxDegreeOfParallelism = flags.PoolSize};
        Var.Log.Param("CONFIGURATION DE LA POOLSIZE : ", "OK");
    }

    #endregion
    
    //
    
    #region Functions

    public static async Task Run(this Structs.SFlags flags)
    {
        var sw = new Stopwatch();
        sw.Start();
        
        Var.Log.Separator("RECHERCHE");

        // TODO: REMETTRE LE BON DOSSIER !
        //await Api.DirSearchAsync(Var.CurrentDir);
        await Api.DirSearchAsync("T:\\- 4 Suivi Appuis\\18-Partage\\de VILLELE DORIAN");
        
        sw.Stop();
        Var.Results.SearchTimer = sw.Elapsed.TotalSeconds;
        
        // TODO: A SUPPRIMER ET A REMPLACER PAR UN BILAN
        Var.Log.VoidPink($"{Var.Results.NbFiles} - {Var.Results.SearchTimer} - {Var.Results.NbFilesTotal} - {Var.Results.NbFolders}");
    }

    #endregion
}