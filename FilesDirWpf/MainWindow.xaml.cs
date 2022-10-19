using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
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
                Words = new []{""},
                Extensions =  new []{""},
                FoldersBlackList =  new []{""},
                FoldersWhiteList =  new []{""},
                PoolSize = 10,
                Casse = false,
                Utf = false,
                Silent = false,
            };

            if (!Directory.Exists(flags.DirPath) || flags.DirPath.Contains(@"\\"))
            {
                var err = new AlerteDlg("ERREUR", "Le chemin est incorrect !");
                err.ShowDialog();
                
                return;
            }

            var arg = "/K " + flags.GetFullReqOfSearch();
            Console.WriteLine(arg);

            Process.Start(@$"T:\- 11 Outils\FilesDir\FD.exe", $"""{arg}""");
        }

        #endregion
    }
}