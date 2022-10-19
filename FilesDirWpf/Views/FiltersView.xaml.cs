using System;
using System.Windows;
using System.Windows.Controls;
using FilesDirWpf.UserControls;

namespace FilesDirWpf.Views;

public partial class FiltersView
{
    #region Statements

    public static FiltersView Instance { get; private set; } = new();
    
    private MyWrapView _mlvWords = new();
    private MyWrapView _mlvExtensions = new();
    private MyWrapView _mlvBlackList = new();
    private MyWrapView _mlvWhiteList = new();

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

        AddInGrid(_mlvWords, 1);
        AddInGrid(_mlvExtensions, 3);
        AddInGrid(_mlvBlackList, 5);
        AddInGrid(_mlvWhiteList, 7);
    }

    public (string[] words, string[] extensions, string[] blackList, string[] whiteList) GetFilters()
    {
        var words = _mlvWords.Lst.ToArray();
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