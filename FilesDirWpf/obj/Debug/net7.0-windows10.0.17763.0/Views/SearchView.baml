   M S B A M L   `   `   `  ????  FilesDirWpf"    FilesDirWpf.Views.SearchView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .??5	      ? $??10, 10q?6     ? RowDefinitions  5
   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??5 ?6   ?? 5      $??* ?6   ?? 5      $??0 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $??0??6   $??5??6-   $??	Recherche??6A      LabelTransparentM2Title#??[  6U   ?? 5   
   $??1??6   $??0??6   $??Dossier de recherche :??6-     LabelTransparentM2#??[ 6N   ?? 5   
   -   5         ??Name$ TextBoxFolder??5      $??1??6)   $??2??66     TextBoxPlainRoundedM2Left#??[ 6F   $
??Left=?6y   $??
3, 0, 0, 0q?6?   ?? 5    
   -   5         ??Name$ ButtonOpenFolder??5       $??1??6+   $??4??68     ButtonPlainM2Right#??[ 5!      $??35??5"      .??5%      ??   ImgfolderOrigin#??[ 6   $??15??5&      $??15??5'      5(      ?? 5,   
   $??3??6   $??0??6   $??Mode de recherche :??6-   #??[ 6K   ?? 5-   
   -   5         ??Name$ ComboBoxSearchMode??5-      $??3??5.      $??2??6    $??3??60     ComboBoxPlainM2#??[ 5/      .@?51      ?? $?? Contient (%)??6   ?? 52      $?? Est égal à (=)??6   ?? 53      $?? Commence par (^)??6   ?? 54      $?? Se termine par ($)??6   ?? 55      $+??% Avancé (En cours de développement)??6   56      ?? 5:   
   -   5         ??Name$ LabelPaterneRegex??5:      $??5??6+   $??0??68   $??	Pattern :??6H   #??[ 5;      $y?Hidden=?6<   ?? 5<   
   -   5        $ TextBoxPaterneRegex??5<      $??5??6/   $??2??6<   $??3??6L     TextBoxPlainRoundedM2#??[ 5=      $
??Left=?6A   $??
3, 0, 0, 0q?6c   $y?Hidden=?5>      5?      5@      