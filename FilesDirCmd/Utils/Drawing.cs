using FilesDir.Utils;
using Logger;

namespace FilesDirCmd.Utils;

public static class Drawing
{
    public static void Start()
    {
        Var.Log.DrawStart(Cst.Logo, Cst.Author, Cst.Version);
    }

    public static void End()
    {
        Var.Log.DrawEnd(Cst.Author, Cst.Version);
        Var.Log.VoidGreen("Appuyer sur ENTREE pour quitter...");
        Console.ReadLine();
    }
}