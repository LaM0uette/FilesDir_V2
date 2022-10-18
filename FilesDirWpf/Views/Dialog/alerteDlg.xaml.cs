using System.Windows;

namespace FilesDirWpf.Views.Dialog;

public partial class AlerteDlg
{
    #region Statements

    public AlerteDlg(string title = "Alerte", string msg = "")
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
