   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfAppOrbit.PagesWpfAppOrbit.Pages  5      +    %WpfAppOrbit.Pages.CatalogProductsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      +localclr-namespace:WpfAppOrbit.Pages  5      -   5          N� Title$  Каталог��5
   	      	base_page#��[  5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $��30 �6   .+�5   
   � $��0��6   $��Stretch=�5        
additional#��[ 5      .+�5      ��  �� Orientation$ 
Horizontal=�6   $��20 5 5 5q�62   .+�5      �� -   5         ��Name$ 
TBoxSearch��5   &   $	��275��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$/ )наименование продукта��5         HintOpacity$	 .26Q�5   !   5      �� 5       $ 
Horizontal=�6   $��20 5 5 5q�63   .+�5"      �� -   5         ��Name$ 	ComboType��5%      $	��275��5"      $ тип продукта��5#      $	 .26Q�5$       �� DisplayMemberPath$ ProductTypeName��5'       � 	Resources 5)        MaterialDesignPaper	 �� Color��%)   &	       �� 5*      $	 White��6F   5+      5,      5-      �� 5.      $ 
Horizontal=�6   $��20 5 5 5q�63   .+�50      �� -   5        $ 	ComboSort��51      $	��275��50      $! Сортировать по��52      $	 .26Q�53      ���#FFF 56      ��%)   &	       �� 57      $	 White��6B   58      .@�59      �� $2��,По возрастанию названия��6   �� 5:      $,��&По убыванию названия��6#   �� 5;      $7��1По возрастанию номера цеха��6#   �� 5<      $/��)По убыванию номера цех��6#   �� 5=      $K��EПо возрастанию минимальной стоимости��6#   �� 5>      $F��@По убыванию  минимальной стоимости��6#   5?      5@      �� 5A      -   5        
 ��Name$
 BtnChangePriority��5A      $I��CИзменить минимальную стоимость на ...��61   
�������$
��Bold"�6u   5B      5C      �� 5D   
   -   5         ��Name$ ListBoxProducts��5D      $��Visible=�6,   $��Stretch=�6b    �� SelectedValuePath$ Id��5E      $��1��6   $��0��6,     	good_item#��[ 6@    �� SelectionMode$ Multiple=�6p   ��5G      �� 5H      .R�5K      �  � ColumnDefinitions 5L      �� 5M      $��60 �6/   �� 5N      $��* �6/   �� 5O      $��* �6/   .+�5Q      �� $��0��6    $��60��60   $��80��6;   ��6G   ��  �� Path$ GetPhoto��� 5R      $��1��6%   $��Stretch=�65   .+�5S      �� $��2q�6(   $��16(�63   $
��Auto��6A   $��Stretch=�6N   ��5T   "   `� 5U   &    �� StringFormat$ 	{0} | {1}��63   .1�5V   *   �� $! ProductType.ProductTypeName�62   �� 5W   *   $ ProductName�62   5X   '   5Z      �� 5[      $��2q�6(   $��16(�63   $
��Auto��6|   $
��Left=�6�   ��6A   �� $ Artikul�$ Артикул: {0}��6�   �� 5\      $��2q�6(   $��16(�63   $
��Auto��6�   $
��Wrap=�6�   $
��Left=�6�   ��6A   �� $ MaterialsItems�$ Материалы: {0}��6�   5^      �� 5_      $	��120��6%   $��Right=�61   $��2��6M   $��16(�6^   $��Center=�6l   $
��Bold"�6�   ��6�   �� *MinimalPrice+$ c�� �� ConverterCulture$ ru��5`      5b      5d      �� 5f   
   $ 
Horizontal=�6   $��2��6.   $��Right=�6;   .+�5g      �� -   5        $
 BtnPrev��5g        MaterialDesignFlatButton#��[ 6&   $��0q�5h      $��0q�6   $��Center=�5i      $	�� 25��5j      ��5g   X   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ ArrowLeftBoldBox=�5j      �� 5k      -   5        $ ListBoxPageCount��5k      $��Right=�5l      $��0q�5m      ��5o      �� 5p      .R�5q      �� $ 
Horizontal=�6%   5r      5t      �� 5u      -	   5        $
 BtnNext��5u      #��[ 6&   $��0q�5v      $��0q�6   $��Center=�5w      $��25��5x      ��5u   X     $ ArrowRightBoldBox=�5x      5y      �� 5z   
   -
   5         ��Name$ TextBlockCount��5z   !   $��3��6   6!   5{      5|      