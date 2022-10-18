using System;
using System.Windows;
using System.Windows.Controls;

namespace FileDirWpf.Views;

public partial class SearchView
{
    #region Statements

    public SearchView()
    {
        InitializeComponent();

        SetInitialValuesComboBox();
    }

    #endregion

    //

    #region Fonctions

    private void SetInitialValuesComboBox()
    {
        ComboBoxSearchMode.SelectedIndex = 0;
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
    }

    #endregion
}