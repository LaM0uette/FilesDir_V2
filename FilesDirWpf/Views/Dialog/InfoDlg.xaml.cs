using System.Windows;

namespace FilesDirWpf.Views.Dialog;

public partial class InfoDlg
{
    #region Statements

    public InfoDlg(string title = "Info", string msg = "")
    {
        InitializeComponent();

        Title = title;
        LblMsg.Text = msg;
    }

    #endregion

    //

    #region Actions

    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    #endregion
}
