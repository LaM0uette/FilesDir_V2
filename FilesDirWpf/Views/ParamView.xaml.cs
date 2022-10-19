namespace FilesDirWpf.Views;

public partial class ParamView
{
    #region Statements
    
    public static ParamView Instance { get; private set; } = new();

    public ParamView()
    {
        Instance = this;
        
        InitializeComponent();
    }

    #endregion

    //

    #region Fonctions

    public (int pool, bool casse, bool utf, bool silent) GetParameters()
    {
        var pool = ComboBoxPool.SelectedIndex switch
        {
            0 => 20,
            1 => 500,
            2 => 3000,
            _ => 500
        };
        
        return (pool, CheckBoxCasse.IsChecked!.Value, CheckBoxUtf.IsChecked!.Value, false);
    }

    #endregion
}