using System.IO;
using System.Windows;
using FilesDir.Flags;
using FilesDirWpf.Views;
using FilesDirWpf.Views.Dialog;
using Microsoft.Toolkit.Uwp.Notifications;

namespace FilesDirWpf
{
    public partial class MainWindow
    {
        #region Statements

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        //

        #region Actions

        private void ButtonRunSearch_OnClick(object sender, RoutedEventArgs e)
        {
            var searchs = SearchView.Instance.GetSearchs();
            var filters = FiltersView.Instance.GetFilters();
            var parameters = ParamView.Instance.GetParameters();

            var flags = new Flags
            {
                SearchMode = searchs.mode,
                Re = searchs.regex,
                DirPath = searchs.folder,
                Words = filters.words,
                Extensions =  filters.extensions,
                FoldersBlackList =  filters.blackList,
                FoldersWhiteList =  filters.whiteList,
                PoolSize = parameters.pool,
                Casse = parameters.casse,
                Utf = parameters.utf,
                Silent = parameters.silent,
            };

            if (!Directory.Exists(flags.DirPath) || flags.DirPath.Contains(@"\\"))
            {
                var err = new AlerteDlg("ERREUR", "Le chemin est incorrect !");
                err.ShowDialog();
                
                return;
            }

            var arg = flags.GetReqOfSearch();
            
            Clipboard.SetText(arg);
            
            new ToastContentBuilder()
                .AddText("Copié dans le presse papier !")
                .AddText(arg)
                .SetToastDuration(ToastDuration.Short)
                .Show();
        }

        #endregion
    }
}


