﻿using System.Windows;
using System.Windows.Controls;
using FilesDir.Core;
using FilesDir.Utils;

namespace FilesDirWpf.Views;

public partial class SearchView
{
    #region Statements
    
    public static SearchView Instance { get; private set; } = new();

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

    public (string folder, MyEnum.SearchMode mode, string regex) GetSearchs()
    {
        var folder = TextBoxFolder.Text;
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
        
        return (folder, mode, regex);
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
            0 => "Intègre les fichiers dont le nom contient le(s) terme(s) saisis.",
            1 => "Intègre les fichiers dont le nom est égal au(x) terme(s) saisis.",
            2 => "Intègre les fichiers dont le nom commence par le(s) terme(s) saisis.",
            3 => "Intègre les fichiers dont le nom se termine par le(s) terme(s) saisis.",
            4 => "Intègre les fichiers dont le nom match avec le(s) terme(s) saisis.",
            _ => "Intègre les fichiers dont le nom contient le(s) terme(s) saisis.",
        };

        FiltersView.Instance.MlvWords.RefreshList();
    }

    #endregion
}