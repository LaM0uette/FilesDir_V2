using System.Windows;
using System.Windows.Controls;
using FilesDirWpf.UserControls;

namespace FilesDirWpf.Views;

public partial class FiltersView
{
    #region Statements
    
    private MyWrapView _mlvWords = new();
    private MyWrapView _mlvExtensions = new();
    private MyWrapView _mlvBlackList = new();
    private MyWrapView _mlvWhiteList = new();

    public FiltersView()
    {
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

        AddInGrid(_mlvWords, 1);
        AddInGrid(_mlvExtensions, 3);
        AddInGrid(_mlvBlackList, 5);
        AddInGrid(_mlvWhiteList, 7);
    }

    #endregion

    //

    #region Actions

    #endregion
}