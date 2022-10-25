namespace FilesDir.Core;

public struct Results
{
    public string Req;

    public TimeSpan TotalTimer;

    public TimeSpan SearchTimer;

    public int NbFiles;
    
    public int NbErrFiles;

    public int NbFilesTotal;

    public int NbFolders;
    
    public int NbErrFolders;
}