using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FilesDir.Flags;
using FilesDir.Utils;
using FilesDirWpf.Utils;
using FilesDirWpf.Views;
using FilesDirWpf.Views.Dialog;
using Microsoft.Toolkit.Uwp.Notifications;

namespace FilesDirWpf
{
    public partial class MainWindow
    {
        #region Statements
        
        private BrushConverter _converter = new ();

        public MainWindow()
        {
            InitializeComponent();
            
            MyEvent.OnParamChanged += ParamChanged;
            ParamChanged();
        }

        #endregion

        //

        #region Fonctions

        private void ParamChanged()
        {
            var filters = FiltersView.Instance.GetFilters();
            var ext = filters.extensions[0].Equals("") || filters.extensions.Length <= 0 ? "*" : filters.extensions[0];
            
            WrapPanelExemples.Children.Clear();
            
            if (filters.words[0].Equals("") || filters.words.Length <= 0)
            {
                WrapPanelExemples.Children.Add(new Label {Content = $"*.{ext}"});
                return;
            }

            var i = 0;
            foreach (var word in filters.words)
            {
                var nWord = word;
                
                if (filters.extensions.Length > 0 && !filters.extensions[0].Equals(""))
                {
                    ext = filters.extensions[i];
                    i++;

                    if (i >= filters.extensions.Length) i = 0;
                }
                else
                {
                    ext = "*";
                }

                if (!filters.casse) nWord = nWord.ToLower();
                if (!filters.utf) nWord = nWord.RemoveAccent();

                var lb = new Label {Content = $" `{nWord}.{ext}` ", FontSize = 14};
                
                if (!word.Equals(nWord) && (filters.casse || filters.utf))
                {
                    lb.Foreground = (Brush) _converter.ConvertFrom("#FF329BA8")!;
                }
                
                WrapPanelExemples.Children.Add(lb);
                
                if (WrapPanelExemples.Children.Count >= 8) return;
            }
        }

        #endregion

        //

        #region Actions

        private void ButtonRunSearch_OnClick(object sender, RoutedEventArgs e)
        {
            var searchs = SearchView.Instance.GetSearchs();
            var filters = FiltersView.Instance.GetFilters();

            var flags = new Flags
            {
                SearchMode = searchs.mode,
                Re = searchs.regex,
                DirPath = searchs.folder,
                Words = filters.words,
                Extensions =  filters.extensions,
                FoldersBlackList =  filters.blackList,
                FoldersWhiteList =  filters.whiteList,
                PoolSize = 500,
                Casse = filters.casse,
                Utf = filters.utf,
                Silent = false,
            };

            if (!Directory.Exists(flags.DirPath) || flags.DirPath.Contains(@"\\"))
            {
                var err = new AlerteDlg("ERREUR", "Le chemin est incorrect !");
                err.ShowDialog();
                
                return;
            }

            var arg = flags.GetFullReqOfSearch();
            
            Process.Start(@"T:\- 11 Outils\FilesDir\FD.exe", arg);
            
            new ToastContentBuilder()
                .AddText("Recherche lancée !")
                .AddText(arg)
                .SetToastDuration(ToastDuration.Short)
                .Show();
        }

        #endregion
    }
}


