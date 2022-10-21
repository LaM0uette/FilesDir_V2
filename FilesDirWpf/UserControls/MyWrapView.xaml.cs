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
    
    private BrushConverter _converter = new ();
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
            txt.GotFocus += RemoveTextB;
            txt.LostFocus += AddTextB;
            txt.KeyDown += TxtAddTag_OnClick;
        
            WrapLst.Children.Add(txt);
            //WrapLst.Children.Add(new Label{Content = PhTitle, FontSize = 14});
            //WrapLst.Children.Add(new Label{Content = PhMsg, FontSize = 12, FontStyle = FontStyles.Italic});
        }

        foreach (var item in Lst)
        {
            var bd = new Border
            {
                Margin = new Thickness(5),
                BorderBrush = (Brush)_converter.ConvertFrom("#FF329BA8")!,
                CornerRadius = new CornerRadius(3),
                BorderThickness = new Thickness(2)
            };
            
            var lb = new Label
            {
                Content = item, 
                Foreground = (Brush)_converter.ConvertFrom("#FFFFFAFF")!,
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
            txt.GotFocus += RemoveText;
            txt.LostFocus += AddText;
            txt.KeyDown += TxtAddTag_OnClick;
        
            WrapLst.Children.Add(txt);
        }
        
        MyEvent.InvokeParamChanged();
    }
    
    private void BtnTag_OnClick(object sender, RoutedEventArgs e)
    {
        var btn = (Button) sender;

        var index = Lst.IndexOf(btn.Tag as string ?? string.Empty);
        Lst.RemoveAt(index);

        RefreshList();
    }

    private void AddTag(string msg)
    {
        if (msg.Equals("") || Lst.Any(msg.Equals)) return;

        Lst.Add(msg);
        RefreshList();
    }
    
    private void Clear()
    {
        WrapLst.Children.Clear();
    }

    #endregion

    //

    #region Actions
    
    private void TxtAddTag_OnClick(object sender, KeyEventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (e.Key.Equals(Key.Enter))
        {
            AddTag(txt.Text);
        }
    }
    
    private void RemoveTextB(object sender, EventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (txt.Text == $"{PhTitle}\n    {PhMsg}") 
        {
            txt.Text = "";
        }
    }

    private void AddTextB(object sender, EventArgs e)
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
    
    private static void RemoveText(object sender, EventArgs e)
    {
        var txt = (TextBox) sender;
        
        if (txt.Text == "Ajouter un mot...") 
        {
            txt.Text = "";
        }
    }

    private void AddText(object sender, EventArgs e)
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

    #endregion
}