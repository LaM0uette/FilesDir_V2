   M S B A M L   `   `   `  ????  FilesDirWpf#    FilesDirWpf.Views.FiltersView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .??5	      ? -   5          ?Name$  	GridLists??5	      $??10, 10q?6    ? RowDefinitions 5
   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6   ?? 5      $??10 ?6   ?? 5      $
??Auto ?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??5 ?6   ?? 5      $??* ?6   ?? 5      $??0 ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $??0??6   $??5??6-   $??Filtres??6A      LabelTransparentM2Title#??[  6S   ?? 5"   
   $??1??6   $??0??6   $??Mots :??6-     LabelTransparentM2#??[ 6>   ?? 5$   
   $??3??6   $??0??6"   $??5??62    ?? Orientation$ 
Horizontal=?5%      $??Right=?5&      $??0, 0, 0, 20q?5'      .+?5*      ?? -   5         ??Name$ CheckBoxCasse??5*      $??Sensible aux Majuscules ???6.    ? ToolTip$: 4Les Majuscules n'ont pas d'impact pour la recherche.??5+      5,      ?? 50      -   5        $ CheckBoxUtf??50      $??Sensible aux accents ???6,   $??15, 0, 45, 0q?51      $7 1Les accents n'ont pas d'impact pour la recherche.??52      53      54      ?? 58   
   $??5??6   $??0??6   $??Extensions :??6-   #??[ 6D   ?? 5<   
   $??7??6   $??0??6   $??BlackList :??6-   #??[ 6C   ?? 5@   
   $??9??6   $??0??6   $??WhiteList :??6-   #??[ 6C   5B      5C      