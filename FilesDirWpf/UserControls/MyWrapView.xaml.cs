using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FilesDirWpf.Utils;

namespace FilesDirWpf.UserControls;

public partial class MyWrapView
{
    #region Statements

    public List<string> Lst = new();
    public string PhTitle;
    public string PhMsg;
    
    public MyWrapView(string phTitle = "", string phMsg = "")
    {
        InitializeComponent();

        PhTitle = phTitle;
        PhMsg = phMsg;

        RefreshList();
    }

    #endregion

    //

    #region Fonctions

    public void RefreshList()
    {
        Clear();

        if (Lst.Count <= 0)
        {
            var txt = new TextBox
            {
                Height = 55,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                AcceptsReturn = false,
                FontSize = 14,
                FontStyle = FontStyles.Italic,
                Text = $"{PhTitle}\n    {PhMsg}"
            };
            
            txt.GotFocus += RemoveTextInit;
            txt.LostFocus += AddTextInit;
            txt.KeyDown += TxtAddTag;
        
            WrapLst.Children.Add(txt);
        }

        foreach (var item in Lst)
        {
            var bd = new Border
            {
                Margin = new Thickness(5),
                BorderBrush = (Brush)Tasks.Converter.ConvertFrom("#FF329BA8")!,
                CornerRadius = new CornerRadius(3),
                BorderThickness = new Thickness(2)
            };
            var lb = new Label
            {
                Content = item, 
                Foreground = (Brush)Tasks.Converter.ConvertFrom("#FFFFFAFF")!,
                FontSize = 12
            };
            var btn = new Button
            {
                Width = 20,
                Height = 20,
                Tag = item
            };
            var img = new Image
            {
                Width = 16,
                Height = 16,
                Source = new BitmapImage(new Uri(@"T:\- 11 Outils\FilesDir\Docs\croix.png"))
            };

            btn.Content = img;
            btn.Click += BtnTag_OnClick;

            var stk = new StackPanel
            {
                Orientation = Orientation.Horizontal, 
                Margin = new Thickness(1, 0, 2, 0)
            };
            
            stk.Children.Add(lb);
            stk.Children.Add(btn);
            
            bd.Child = stk;

            WrapLst.Children.Add(bd);
        }
        
        if (Lst.Count > 0)
        {
            var txt = new TextBox
            {
                Width = 120,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Text = "Ajouter un mot..."
            };
            
            txt.GotFocus += RemoveTextTag;
            txt.LostFocus += AddTextTag;
            txt.KeyDown += TxtAddTag;
        
            WrapLst.Children.Add(txt);

            txt.Focus();
        }
        
        MyEvent.InvokeParamChanged();
    }
    
    private void Clear()
    {
        WrapLst.Children.Clear();
    }

    private void AddTag(string msg)
    {
        if (msg.Equals("") || Lst.Any(msg.Equals)) return;

        Lst.Add(msg);
        RefreshList();
    }

    #endregion

    //

    #region Actions
    
    private void RemoveTextInit(object sender, EventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (txt.Text == $"{PhTitle}\n    {PhMsg}") 
        {
            txt.Text = "";
        }
    }
    private void AddTextInit(object sender, EventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (string.IsNullOrWhiteSpace(txt.Text))
            txt.Text = $"{PhTitle}\n    {PhMsg}";
        else
        {
            if (txt.Text.Equals("") || Lst.Any(txt.Text.Equals))
            {
                txt.Text = "Ajouter un mot...";
                return;
            }
            AddTag(txt.Text);
        }
    }
    
    private static void RemoveTextTag(object sender, EventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (txt.Text == "Ajouter un mot...") 
        {
            txt.Text = "";
        }
    }
    private void AddTextTag(object sender, EventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (string.IsNullOrWhiteSpace(txt.Text))
            txt.Text = "Ajouter un mot...";
        else
        {
            if (txt.Text.Equals("") || Lst.Any(txt.Text.Equals))
            {
                txt.Text = "Ajouter un mot...";
                return;
            }
            AddTag(txt.Text);
        }
    }
    
    private void TxtAddTag(object sender, KeyEventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (e.Key.Equals(Key.Enter))
        {
            AddTag(txt.Text);
        }
    }
    
    private void BtnTag_OnClick(object sender, RoutedEventArgs e)
    {
        var btn = (Button) sender;

        var index = Lst.IndexOf(btn.Tag as string ?? string.Empty);
        Lst.RemoveAt(index);

        RefreshList();
    }

    #endregion
}