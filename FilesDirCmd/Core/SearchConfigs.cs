using FilesDir.Core;
using FilesDir.Interfaces;
using FilesDir.Utils;

namespace FilesDirCmd.Core;

public static partial class Search
{
    public static void Init(this IFlags flags)
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.Param("INITIALISATION DE LA RECHERCHE EN COURS...");

        Var.Log.Param("CREATION DE LA REQUETE EN COURS...");
        Var.Results.Req = flags.GetReqOfSearch();
        
        Var.Log.Param("REQUETE UTILISEE : ", Var.Results.Req);
        
        Var.Log.Param("INITIALISATION DU FICHIER DUMP...");
        Var.Dump.String("Id;Fichier;Date creation;Date de modification;Lien du fichier;Lien du dossier");
    }

    public static void CheckPoolSize(this IFlags flags)
    {
        if (flags.PoolSize < 2)
        {
            Var.Log.Nok("Poolsize trop petite, impossible de mettre moins de 2");
            Var.Log.Param("Modification de la Poolsize en cours...");
            flags.PoolSize = 2;
        }
        
        Var.Log.Param("POOLSIZE MISE A : ", flags.PoolSize.ToString());
    }

    public static void SetPoolSize(this IFlags flags)
    {
        Var.ParallelOptions = new ParallelOptions {MaxDegreeOfParallelism = flags.PoolSize};
        Var.Log.Param("CONFIGURATION DE LA POOLSIZE : ", "OK");
    }
}