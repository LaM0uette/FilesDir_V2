using System.Windows;

namespace FilesDirWpf.Views.Dialog;

public partial class TextBoxDlg
{
    #region Statements

    public string Msg = "";

    public TextBoxDlg(string title = "Info", string msg = "")
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
        Msg = LblMsg.Text;
        
        Close();
    }

    #endregion
}
