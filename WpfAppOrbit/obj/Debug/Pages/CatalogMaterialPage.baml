   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfAppOrbit.PagesWpfAppOrbit.Pages  5      +    %WpfAppOrbit.Pages.CatalogMaterialPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      +localclr-namespace:WpfAppOrbit.Pages  5      -   5          N� Title$  Каталог��5
         	base_page#��[  5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $��30 �6   .+�5   
   � $��0��6   $��Stretch=�6!   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��20 5 5 5q�62   .+�5      �� -   5         ��Name$ 
TBoxSearch��5   &   $	��275��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$1 +наименование материала��5         HintOpacity$	 .26Q�5   !   5      �� 5      $ 
Horizontal=�6   $��20 5 5 5q�63   .+�5!      �� -   5         ��Name$ 	ComboType��5$      $	��275��5!      $ тип материала��5"      $	 .26Q�5#       �� DisplayMemberPath$ MaterialTypeName��5&       � 	Resources 5(        MaterialDesignPaper	 �� Color��%)   &	       �� 5)      $	 White��6F   5*      5+      5,      �� 5.      $ 
Horizontal=�6   $��20 5 5 5q�63   .+�50      �� -   5        $ 	ComboSort��51      $	��275��50      $! Сортировать по��52      $	 .26Q�53      ���#FFF 56      ��%)   &	       �� 57      $	 White��6F   58      .@�59      �� $:��4По возрастанию наименования��6#   �� 5:      $4��.По убыванию наименования��6#   �� 5;      $B��<По возрастанию остатка на складе��6#   �� 5<      $<��6По убыванию остатка на складе��6#   �� 5=      $4��.По возрастанию стоимости��6#   �� 5>      $/��)По убыванию  стоимости��6#   5?      5@      �� 5A      -   5        
 ��Name$
 BtnChangePriority��5A      $K��EИзменить минимальное количество на ...��61   5B      5D      �� 5F   
   -   5         ��Name$ ListBoxMaterials��5F      $��Visible=�5G       �� SelectedValuePath$ 
MaterialId��5H      $��1��5I      $��0��6    �� SelectionMode$ Multiple=�5J        	good_item#��[ 5K      ��5M      �� 5N      .R�5O      � $��Stretch=�6    � UseLayoutRounding .  � ColumnDefinitions 5P      �� 5Q      $��60 �6/   �� 5R      $��* �6/   �� 5S      $	��200 �6/   .+�5U      �� $��0��6    $��60��60   $��40��6;   $��Uniform=�5V      $��5 0 5 0q�5W      ��5V      ��  �� Path$ GetPhoto�5W      �� 5X      $��1��6%   .+�5Y      �� $��2q�6(   $��16(�63   $
��Auto��5[      $
��Left=�6   ��5Z      �� $ GetInfo�5[      �� 5\      $��2q�6(   $��16(�63   $
��Auto��5^      $
��Left=�6   ��5]      �� $ GetMinimumCount�5^      �� 5_      $��2q�6(   $��16(�63   $
��Auto��5a      $
��Left=�6   ��5`      �� $ GetSuppliers�5a      5c      �� 5d      $	��200��6%   $��Center=�61   $��2��5e      $��5q�6   $��16(�6    ��5f      �� $ GetStoreCount�5g      5h      5j      �� 5l   
   $ 
Horizontal=�6   $��2��6.   $��Right=�6;   .+�5m      �� -   5        $
 BtnPrev��5m        MaterialDesignFlatButton#��[ 6&   $��0q�5n      $��0q�6   $��Center=�5o      $	�� 25��5p      ��5m   X   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ ArrowLeftBoldBox=�5p      �� 5q      -   5        $ ListBoxPageCount��5q      $��Right=�5r      $��0q�5s      ��5u      �� 5v      .R�5w      �� $ 
Horizontal=�6%   5x      5z      �� 5{      -	   5        $
 BtnNext��5{      #��[ 6&   $��0q�5|      $��0q�6   $��Center=�5}      $��25��5~      ��5{   X     $ ArrowRightBoldBox=�5~      5      �� 5�   
   -
   5         ��Name$ TextBlockCount��5�   !   $��3��6   6!   5�      5�      