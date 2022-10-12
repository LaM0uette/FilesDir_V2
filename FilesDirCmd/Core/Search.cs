using FilesDir.Core;
using FilesDir.Utils;
using FilesDirCmd.Utils;

namespace FilesDirCmd.Core;

public static class Search
{
    public static void Init(this Structs.SFlags flags)
    {
        Var.Log.Separator("PARAMETRES");
        Var.Log.Param("INITIALISATION DE LA RECHERCHE EN COURS...");
        
        Var.Log.Param("REQUETE UTILISEE : ", flags.GetReqOfSearch());
    }
}