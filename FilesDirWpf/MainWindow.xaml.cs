using System.Diagnostics;
using System.IO;
using System.Windows;
using FilesDir.Flags;
using FilesDirWpf.Views;
using FilesDirWpf.Views.Dialog;

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

            var flags = new Flags
            {
                SearchMode = searchs.mode,
                Re = searchs.regex,
                DirPath = searchs.folder,
                // Words = 
                // Extensions = 
                // FoldersBlackList = 
                // FoldersWhiteList = 
                // PoolSize = 
                // Casse = 
                // Utf = 
                // Silent = 
            };
            
            if (!Directory.Exists(flags.DirPath) || flags.DirPath.Contains(@"\\"))
            {
                var err = new AlerteDlg("ERREUR", "Le chemin est incorrect !");
                err.ShowDialog();
                
                return;
            }
            
            var arg = "/k" + flags.GetFullReqOfSearch();
            
            Process.Start(@"T:\- 11 Outils\FilesDir\FD.exe", arg);
        }

        #endregion
    }
}