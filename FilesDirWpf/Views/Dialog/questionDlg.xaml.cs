using System.Windows;

namespace FilesDirWpf.Views.Dialog;

public partial class QuestionDlg
{
    #region Statements

    public bool Result;

    public QuestionDlg(string title = "?", string msg = "", string txtOk = "Valider", string txtNok = "Annuler")
    {
        InitializeComponent();

        Title = title;
        LblMsg.Text = msg;
        ButtonValider.Content = txtOk;
        ButtonAnnuler.Content = txtNok;
    }

    #endregion

    //

    #region Actions

    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        Result = true;
        
        Close();
    }
    
    private void ButtonNok_OnClick(object sender, RoutedEventArgs e)
    {
        Result = false;
        
        Close();
    }

    #endregion
}
