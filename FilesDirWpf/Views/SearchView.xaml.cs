using System.Windows;
using System.Windows.Controls;
using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDirWpf.Views;

public partial class SearchView
{
    #region Statements
    
    public static SearchView Instance { get; private set; } = new();
    public record Searchs(string Folder, MyEnum.SearchMode Mode, string Regex);

    public SearchView()
    {
        Instance = this;
        
        InitializeComponent();

        SetInitialValues();
    }

    #endregion

    //

    #region Fonctions

    private void SetInitialValues()
    {
        TextBoxFolder.Text = Var.CurrentDir;
        ComboBoxSearchMode.SelectedIndex = 0;
    }

    public Searchs GetSearchs()
    {
        var folder = TextBoxFolder.Text.ToLower().Replace(@"\\BORDEAUX14\Agence".ToLower(), "G:".ToLower());
        var regex = TextBoxPaterneRegex.Text;

        var mode = ComboBoxSearchMode.SelectedIndex switch
        {
            0 => MyEnum.SearchMode.In,
            1 => MyEnum.SearchMode.Equal,
            2 => MyEnum.SearchMode.Begin,
            3 => MyEnum.SearchMode.End,
            4 => MyEnum.SearchMode.Re,
            _ => MyEnum.SearchMode.In
        };

        return new Searchs(
            folder, 
            mode, 
            regex
        );
    }

    #endregion

    //

    #region Actions

    private void ComboBoxSearchMode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var idx = ComboBoxSearchMode.SelectedIndex;
        
        if (idx.Equals(4))
        {
            LabelPaterneRegex.Visibility = Visibility.Visible;
            TextBoxPaterneRegex.Visibility = Visibility.Visible;
        }
        else
        {
            LabelPaterneRegex.Visibility = Visibility.Hidden;
            TextBoxPaterneRegex.Visibility = Visibility.Hidden;
        }
        
        FiltersView.Instance.MlvWords.PhTitle = ComboBoxSearchMode.SelectedIndex switch
        {
            0 => "Saisir le(s) mots(s) devant être contenus dans les noms des fichiers recherchés.",
            1 => "Saisir le(s) mot(s) correspondant exactement aux noms des fichiers recherchés.",
            2 => "Saisir le(s) mot(s) devant être situés au début des noms des fichiers recherchés.",
            3 => "Saisir le(s) mot(s) devant être situés à la fin des noms des fichiers recherchés.",
            4 => "Saisir le(s) mots(s) devant match dans les noms des fichiers recherchés.",
            _ => "Saisir le(s) mots(s) devant être contenus dans les noms des fichiers recherchés.",
        };
        
        FiltersView.Instance.MlvWords.RefreshList();
    }

    private void ButtonOpenFolder_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        
        if (dialog.ShowDialog().GetValueOrDefault()) TextBoxFolder.Text = dialog.SelectedPath;
    }

    #endregion
}