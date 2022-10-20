using System.Windows;
using System.Windows.Controls;
using FilesDirWpf.UserControls;

namespace FilesDirWpf.Views;

public partial class FiltersView
{
    #region Statements

    public static FiltersView Instance { get; private set; } = new();

    public MyWrapView MlvWords = new("Intègre les fichiers dont le nom contient le(s) terme(s) saisis.", """Ex : "appui", "C3A", "fiche appui", "etudes", "retour travaux", ...""");
    private MyWrapView _mlvExtensions = new("Restreint la recherche à des extensions de fichiers spécifiques.", """Ex : "jpg", "jpeg", "png", "xlsx", "pdf", ...""");
    private MyWrapView _mlvBlackList = new("Exclut des dossiers spécifiques à la recherche.                  ", """Ex : "Old", "Archives", ...              """);
    private MyWrapView _mlvWhiteList = new("Restreint des dossiers spécifiques à la recherche.               ", """Ex : "retour", "retour terrain", ...              """);

    public FiltersView()
    {
        Instance = this;
        InitializeComponent();

        CreateUiList();
    }

    #endregion

    //

    #region Fonctions
    
    private void CreateUiList()
    {
        void AddInGrid(UIElement lst, int row)
        {
            Grid.SetRow(lst, row);
            Grid.SetColumn(lst, 2);
            Grid.SetColumnSpan(lst, 3);
            
            GridLists.Children.Add(lst);
        }

        AddInGrid(MlvWords, 1);
        AddInGrid(_mlvExtensions, 3);
        AddInGrid(_mlvBlackList, 5);
        AddInGrid(_mlvWhiteList, 7);
    }

    public (string[] words, string[] extensions, string[] blackList, string[] whiteList) GetFilters()
    {
        var words = MlvWords.Lst.ToArray();
        var extensions = _mlvExtensions.Lst.ToArray();
        var blackList = _mlvBlackList.Lst.ToArray();
        var whiteList = _mlvWhiteList.Lst.ToArray();

        return (
            words.Length <= 0 ? new []{""} : words,
            extensions.Length <= 0 ? new []{""} : extensions,
            blackList.Length <= 0 ? new []{""} : blackList,
            whiteList.Length <= 0 ? new []{""} : whiteList
            );
    }

    #endregion
}