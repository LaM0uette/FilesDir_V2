using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            var ext = filters.Extensions[0].Equals("") || filters.Extensions.Length <= 0 ? "*" : filters.Extensions[0];
            
            Clear();
            
            if (filters.Words[0].Equals("") || filters.Words.Length <= 0)
            {
                WrapPanelExemples.Children.Add(new Label {Content = $"*.{ext}"});
                return;
            }

            GenerateExemplesWords(filters);
        }

        private void Clear()
        {
            WrapPanelExemples.Children.Clear();
        }

        private void GenerateExemplesWords(FiltersView.Filters filters)
        {
            var i = 0;
            foreach (var word in filters.Words)
            {
                var newWord = word;
                string ext;
                
                if (filters.Extensions.Length > 0 && !filters.Extensions[0].Equals(""))
                {
                    ext = filters.Extensions[i];
                    i++;

                    if (i >= filters.Extensions.Length) i = 0;
                }
                else
                {
                    ext = "*";
                }

                if (!filters.Casse) newWord = newWord.ToLower();
                if (!filters.Utf) newWord = newWord.RemoveAccent();

                var lb = new Label {Content = $" `{newWord}.{ext}` ", FontSize = 14};
                
                if (filters.Casse)
                {
                    var w = word.RemoveAccent();
                    var wb = newWord.RemoveAccent();
                    
                    if (!w.ToLower().Equals(wb))
                    {
                        lb.Foreground = (Brush)Utils.Tasks.Converter.ConvertFrom("#FF329BA8")!;
                    }
                }
                if (filters.Utf)
                {
                    var w = word.ToLower();
                    var wb = newWord.ToLower();
                    
                    if (!w.RemoveAccent().Equals(wb))
                    {
                        lb.Foreground = (Brush)Utils.Tasks.Converter.ConvertFrom("#FF329BA8")!;
                    }
                }

                WrapPanelExemples.Children.Add(lb);
                
                if (WrapPanelExemples.Children.Count >= 8) return;
            }
        }

        private static Flags GetFlags()
        {
            var searchs = SearchView.Instance.GetSearchs();
            var filters = FiltersView.Instance.GetFilters();

            return new Flags
            {
                SearchMode = searchs.Mode,
                Re = searchs.Regex,
                DirPath = searchs.Folder,
                Words = filters.Words,
                Extensions =  filters.Extensions,
                FoldersBlackList =  filters.BlackList,
                FoldersWhiteList =  filters.WhiteList,
                PoolSize = 500,
                Casse = filters.Casse,
                Utf = filters.Utf,
                Silent = false,
            };
        }

        #endregion

        //

        #region Actions

        private void ButtonRunSearch_OnClick(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var flags = GetFlags();

            if (!Directory.Exists(flags.DirPath) || flags.DirPath.Contains(@"\\"))
            {
                Mouse.OverrideCursor = null;
                
                var err = new AlerteDlg("ERREUR", "Le chemin est incorrect !");
                err.ShowDialog();
                
                return;
            }

            var arg = flags.GetReqOfSearch(true);
            
            new ToastContentBuilder()
                .AddText("Recherche lancée !")
                .AddText(arg)
                .SetToastDuration(ToastDuration.Short)
                .Show();
            
            Process.Start(@"T:\- 11 Outils\FilesDir\FilesDir.exe", arg);

            Mouse.OverrideCursor = null;
        }

        #endregion
    }
}