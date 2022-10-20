using FilesDir.Utils;
using Logger;

namespace FilesDir.Flags;

public partial class Flags
{
    private void Init()
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.DrawParam("INITIALISATION DE LA RECHERCHE EN COURS...");
        Var.Log.DrawParam("DOSSIER DE RECHERCHE : ", DirPath);

        Var.Log.DrawParam("CREATION DE LA REQUETE EN COURS...");
        Var.Results.Req = GetReqOfSearch();
        
        Var.Log.DrawParam("REQUETE UTILISEE : ", Var.Results.Req);
        
        Var.Log.DrawParam("INITIALISATION DU FICHIER DUMP...");
        Var.Dump.String("Id;Fichier;Date creation;Date de modification;Date d'accès;Lien du fichier;Lien du dossier");
        
        Var.Log.DrawParam("CREATION DU DOSSIER D'EXPORT...");
        Tasks.CreateDir(Path.Join(Var.DirTemp, "exports"));
    }

    private void CheckPoolSize()
    {
        if (PoolSize < 2)
        {
            Var.Log.Nok("Poolsize trop petite, impossible de mettre moins de 2");
            Var.Log.DrawParam("Modification de la Poolsize en cours...");
            PoolSize = 2;
        }
        
        Var.Log.DrawParam("POOLSIZE MISE A : ", PoolSize.ToString());
    }

    private void SetPoolSize()
    {
        Var.ParallelOptions = new ParallelOptions {MaxDegreeOfParallelism = PoolSize};
        Var.Log.DrawParam("CONFIGURATION DE LA POOLSIZE : ", "OK");
    }
}