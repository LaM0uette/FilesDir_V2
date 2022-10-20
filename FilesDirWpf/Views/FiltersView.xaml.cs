using System.Windows;
using System.Windows.Controls;
using FilesDirWpf.UserControls;
using FilesDirWpf.Utils;

namespace FilesDirWpf.Views;

public partial class FiltersView
{
    #region Statements

    public static FiltersView Instance { get; private set; } = new();

    public MyWrapView MlvWords = new("Intègre les fichiers dont le nom contient le(s) terme(s) saisis.", """Ex : "appui", "C3A", "fiche appui", "etudes", "retour travaux", ...""");
    private MyWrapView _mlvExtensions = new("Restreint la recherche à des extensions de fichiers spécifiques.", """Ex : "jpg", "jpeg", "png", "xlsx", "pdf", ...""");
    private MyWrapView _mlvBlackList = new("Exclut des dossiers/sous dossiers spécifiques à la recherche.       ", """Ex : "Old", "Archives", ...              """);
    private MyWrapView _mlvWhiteList = new("Restreint la recherche à des dossiers/sous dossiers spécifiques.     ", """Ex : "retour", "retour terrain", ...              """);

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
        AddInGrid(_mlvExtensions, 5);
        AddInGrid(_mlvBlackList, 7);
        AddInGrid(_mlvWhiteList, 9);
    }

    public (string[] words, bool casse, bool utf, string[] extensions, string[] blackList, string[] whiteList) GetFilters()
    {
        var words = MlvWords.Lst.ToArray();
        var extensions = _mlvExtensions.Lst.ToArray();
        var blackList = _mlvBlackList.Lst.ToArray();
        var whiteList = _mlvWhiteList.Lst.ToArray();

        return (
            words.Length <= 0 ? new []{""} : words,
            CheckBoxCasse.IsChecked!.Value, 
            CheckBoxUtf.IsChecked!.Value,
            extensions.Length <= 0 ? new []{""} : extensions,
            blackList.Length <= 0 ? new []{""} : blackList,
            whiteList.Length <= 0 ? new []{""} : whiteList
            );
    }

    #endregion

    //
    
    #region Actions

    private void CheckBoxCasse_OnClick(object sender, RoutedEventArgs e)
    {
        MyEvent.InvokeParamChanged();

        if (CheckBoxCasse.IsChecked.Equals(true))
            CheckBoxCasse.ToolTip = "Les mots clés doivents respecter les Majuscules des fichiers (Ex: 'Fiche Appuis' ne match pas avec 'fiche appui')";
        else
            CheckBoxCasse.ToolTip = "Les Majuscules n'ont pas d'impact pour la recherche.";
    }

    private void CheckBoxUtf_OnClick(object sender, RoutedEventArgs e)
    {
        MyEvent.InvokeParamChanged();
        
        if (CheckBoxUtf.IsChecked.Equals(true))
            CheckBoxUtf.ToolTip = "Les mots clés doivents respecter les accents des fichiers (Ex: 'études' ne match pas avec 'etudes')";
        else
            CheckBoxUtf.ToolTip = "Les accents n'ont pas d'impact pour la recherche.";
    }

    #endregion
}