﻿using System.Globalization;
using FilesDir.Interfaces;
using FilesDir.Utils;
using Logger;

namespace FilesDirCmd.Utils;

public static class Drawing
{
    public static void Start()
    {
        Var.Log.DrawStart(Cst.Logo, Cst.Author, Cst.Version);
    }
    
    public static void Bilan(this IFlags flags)
    {
        Var.Log.Del();
        
        Var.Log.Separator("BILAN");
        
        Var.Log.Category("INFOS GENERALES");
        Var.Log.SubCategory("Dossier de recherche", flags.DirPath);
        Var.Log.SubCategory("Requête utilisée", flags.GetReqOfSearch());
        Var.Log.Space();
        
        Var.Log.Category("RESULTATS DE LA RECHERCHE");
        Var.Log.SubCategory("Dossiers traités", Var.Results.NbFolders.ToString());
        Var.Log.SubCategory("Fichiers traités", Var.Results.NbFilesTotal.ToString());
        Var.Log.SubCategory("Fichiers trouvés", Var.Results.NbFiles.ToString());
        Var.Log.Space();
        
        Var.Log.Category("TEMPS D'EXECUTIONS");
        Var.Log.SubCategory("Temps total", Var.Results.TotalTimer.ToString(CultureInfo.InvariantCulture));
        Var.Log.SubCategory("Temps de la recherche", Var.Results.SearchTimer.ToString(CultureInfo.InvariantCulture));
        Var.Log.Space();
        
        Var.Log.Category("EXPORTS");
        Var.Log.SubCategory("Logs", $"{Var.DirTemp}\\logs");
        Var.Log.SubCategory("Dumps", $"{Var.DirTemp}\\dumps");
        Var.Log.SubCategory("Exports Excel", $"{Var.DirTemp}\\exports");
        
        End();
    }

    public static void End()
    {
        Var.Log.DrawEnd(Cst.Author, Cst.Version);
        Var.Log.VoidGreen("Appuyer sur ENTREE pour quitter...");
        Console.ReadLine();
    }
}