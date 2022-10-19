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
            Process.Start(@"T:\- 11 Outils\FilesDir\FD.exe", "-w=NPOI");

            // var flag = new Flags
            // {
            //
            // };
            //
            // await flag.RunFilesDir();
        }

        #endregion
    }
}