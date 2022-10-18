using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FilesDirWpf.Views.Dialog;

namespace FilesDirWpf.UserControls;

public partial class MyWrapView
{
    #region Statements

    public List<object> Lst = new();
    
    private BrushConverter _converter = new ();
    
    public MyWrapView()
    {
        InitializeComponent();
    }

    #endregion

    //

    #region Fonctions

    private void RefreshList()
    {
        Clear();

        foreach (var item in Lst)
        {
            var bd = new Border
            {
                Margin = new Thickness(10, 0, 0, 0),
                BorderBrush = (Brush)_converter.ConvertFrom("#FF8F8FA3")!,
                CornerRadius = new CornerRadius(3),
                BorderThickness = new Thickness(1)
            };
            
            var lb = new Label
            {
                Content = item, 
                Foreground = Brushes.White, 
                FontSize = 12
            };
            var btn = new Button
            {
                Width = 20,
                Height = 20,
            };
            var img = new Image
            {
                Width = 16,
                Height = 16,
                Source = new BitmapImage(new Uri(@"T:\- 11 Outils\FilesDir\Docs\croix.png"))
            };

            btn.Content = img;

            var stk = new StackPanel
            {
                Orientation = Orientation.Horizontal, 
                Margin = new Thickness(1, 0, 1, 0)
            };
            
            stk.Children.Add(lb);
            stk.Children.Add(btn);
            
            bd.Child = stk;

            WrapLst.Children.Add(bd);
        }
    }
    
    private void Clear()
    {
        WrapLst.Children.Clear();
    }

    #endregion

    //

    #region Actions

    private void BtnPlus_OnClick(object sender, RoutedEventArgs e)
    {
        var txt = new TextBoxDlg();
        txt.ShowDialog();
        
        if (txt.Msg.Equals("")) return;
        
        Lst.Add(txt.Msg);
        RefreshList();
    }

    #endregion
}