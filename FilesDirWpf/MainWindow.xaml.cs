using System.Diagnostics;
using System.Windows;

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
            var arg = $"""
            -p="{"C:\\"}"
            -w=NPOI
            """;
            
            Process.Start(@"T:\- 11 Outils\FilesDir\FD.exe", arg);
        }

        #endregion
    }
}