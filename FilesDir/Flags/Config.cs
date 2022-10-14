using FilesDir.Utils;

namespace FilesDir.Flags;

public partial class Flags
{
    private void Init()
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.Param("INITIALISATION DE LA RECHERCHE EN COURS...");

        Var.Log.Param("CREATION DE LA REQUETE EN COURS...");
        Var.Results.Req = GetReqOfSearch();
        
        Var.Log.Param("REQUETE UTILISEE : ", Var.Results.Req);
        
        Var.Log.Param("INITIALISATION DU FICHIER DUMP...");
        Var.Dump.String("Id;Fichier;Date creation;Date de modification;Lien du fichier;Lien du dossier");
        
        Var.Log.Param("CREATION DU DOSSIER D'EXPORT...");
        Tasks.CreateDir(Path.Join(Var.DirTemp, "exports"));
    }

    private void CheckPoolSize()
    {
        if (PoolSize < 2)
        {
            Var.Log.Nok("Poolsize trop petite, impossible de mettre moins de 2");
            Var.Log.Param("Modification de la Poolsize en cours...");
            PoolSize = 2;
        }
        
        Var.Log.Param("POOLSIZE MISE A : ", PoolSize.ToString());
    }

    private void SetPoolSize()
    {
        Var.ParallelOptions = new ParallelOptions {MaxDegreeOfParallelism = PoolSize};
        Var.Log.Param("CONFIGURATION DE LA POOLSIZE : ", "OK");
    }
}