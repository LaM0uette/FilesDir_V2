using FilesDir.Core;

namespace FilesDir.Interfaces;

public interface IFlags
{
    #region Statements

    public  MyEnum.SearchMode SearchMode { get; set; }
    public  string Re { get; set; }
    public  string[] Words { get; set; }
    public  string[] Extensions { get; set; }
    public  string[] FoldersBlackList { get; set; }
    public  string[] FoldersWhiteList { get; set; }
    public  int PoolSize { get; set; }
    public  bool Casse { get; set; }
    public  bool Utf { get; set; }
    public  bool Silent { get; set; }

    #endregion

    //

    #region Fonctions

    public Task Run();

    public Task DirSearchAsync(string sDir);

    public Task FileSearchAsync(string sDir);

    public bool CheckFolder(string folder);

    public void CheckFile(FileInfo fi);

    public bool FolderInFilter(string folder);

    public bool FileInFilter(FileInfo fi);
    
    public string CheckFileCasse(string fileName);
    public string CheckFileEncoding(string fileName);
    public bool CheckFileIsClosed(string fileName);
    public bool CheckSearchMode(string fileName);

    #endregion
}