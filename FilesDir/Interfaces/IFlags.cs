using FilesDir.Core;

namespace FilesDir.Interfaces;

public interface IFlags
{
    #region Statements

    public  MyEnum.SearchMode SearchMode { get; set; }
    public  string Re { get; set; }
    public  string DirPath { get; set; }
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

    public Task RunFilesDir();

    public string GetReqOfSearch();
    
    public string GetFullReqOfSearch();

    #endregion
}