using System.Collections;
using System.Linq;
using System.Windows;

namespace FilesDirWpf.UserControls;

public partial class MyListView
{
    #region Statements

    public MyListView()
    {
        InitializeComponent();
    }

    #endregion

    //

    #region Fonctions

    private void Clear() => LstBx.Items.Clear();

    private void SetListData(IEnumerable lst)
    {
        foreach (var str in lst)
        {
            LstBx.Items.Add(str);
        }
    }

    #endregion
    
    //

    #region Actions

    private void BtnPlus_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void BtnMinus_OnClick(object sender, RoutedEventArgs e)
    {
        var selected = LstBx.SelectedItems.Cast<object>().ToArray();
        foreach (var item in selected) LstBx.Items.Remove(item);
    }

    private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
    {
        Clear();
    }

    private void BtnCleen_OnClick(object sender, RoutedEventArgs e)
    {
        Clear();
    }

    #endregion
}