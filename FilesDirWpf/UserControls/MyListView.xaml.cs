using System.Linq;
using System.Windows;
using FilesDirWpf.Views.Dialog;

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

    #region Actions

    private void BtnPlus_OnClick(object sender, RoutedEventArgs e)
    {
        var txt = new TextBoxDlg();
        txt.ShowDialog();
        
        if (txt.Msg.Equals("")) return;
        
        LstBx.Items.Add(txt.Msg);
    }

    private void BtnMinus_OnClick(object sender, RoutedEventArgs e)
    {
        var selected = LstBx.SelectedItems.Cast<object>().ToArray();
        foreach (var item in selected) LstBx.Items.Remove(item);
    }

    #endregion
}