   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfAppOrbit.PagesWpfAppOrbit.Pages  5      &     WpfAppOrbit.Pages.AddProductPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f43xtk)http://schemas.xceed.com/wpf/xaml/toolkit  5      +localclr-namespace:WpfAppOrbit.Pages  5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  65     N� Title$:  4Добавление и редактирование��5         	base_page#��[  6)   .��5      �  � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      .+�5    
   �� $��0��6   $��2��6!   �� 5#   
   -   5         ��Name$ BtnSave��5#      $��12��6"   $��Сохранить��60   $��Center=�6E   $��140,5,340,0q�6b   $	��Top=�6w   $	��120��6�   $��1��5$   &   �� 5&   
   $��0��6   $��0��6"   $��Изображение��62   $
��20 0q�6F   �� 5'   
   $��1��6   $��0��6!   $��Артикул��61   $
��20 0q�6A   �� 5(   
   $��2��6   $��0��6!   $��Название��61   $
��20 0q�6B   �� 5)   
   $��3��6   $��0��6!   $��Описание��61   $
��20 0q�6B   �� 5*   
   $��4��6   $��0��6!   $��Тип ��61   $
��20 0q�6>   �� 5+   
   $��5��6   $��0��6!   $��Размеры ��61   $
��20 0q�6B   �� 5,   
   $��6��6   $��0��6!   $��Вес ��61   $
��20 0q�6>   �� 5-   
   $��7��6   $��0��6!   $��
Цех №��61   $
��20 0q�6?   �� 5.   
   $��5��6    �� Orientation$ 
Horizontal=�6"   $��1��6;   .+�5/      �� $��
Длина��6   $
��10 0q�6'   $��60��65   %  Xceed.Wpf.Toolkit.IntegerUpDown  50      -   5          Name$ UpDownLength��50       $��80��6R   $
��Left=�6]   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 68   �� *Length+6]   �� 51      $��Высота��6   $
��10 0q�6(   $��60��67     52      -   5        $ UpDownHeight��52       $��80��6Q   $
��Left=�6\    68   �� *Height+6\   �� 53      $��Ширина��6   $
��10 0q�6(   $��60��67     54      -   5        $ UpDownWidth��54       $��80��6P   $
��Left=�6[    67   �� *Width+6[   57      �� 58   
   $��6��6   $ 
Horizontal=�6"   $��1��6;   .+�59      �� $��
нетто��6   $��60��6'   $
��10 0q�62     5:      -   5        $ UpDownNetto��5:       $��6��6O   $��1��6\   $��80��6l   $
��Left=�6w    67   �� *Netto+6w   �� 5;      $��брутто��6   $��60��6'   $
��10 0q�62     5<      -   5        $ UpDownBrutto��5<       $��8��6Q   $��1��6^   $��80��6n   $
��Left=�6y    68   �� *Brutto+6y   5=      �� 5>   
   $��8��6   $��0��6!   $C��=Минимальная стоимость для агента��61   $
��20 0q�6Z   $
��Wrap=�6h   �� 5?   
   $��9��6   $��0��6!   $E��?Количество людей для производства��61   $
��20 0q�6[   $
��Wrap=�6i   �� 5@   
   $��10��6   $��0��6"   $2��,Время изготовления, мин.��62   $
��20 0q�6S   $
��Wrap=�6a   �� 5A   
   $��11��6   $��0��6"   $��Матариалы��62   $
��20 0q�6D   $
��Wrap=�6R   �� 5B   
   -   5         ��Name$ TextBoxArtikul��5B      $��1��6D   $��1��6Q   $	��0,2q�6a   ��6*   �� *	Artikul+6a   �� 5C   
   -   5        $ TextBoxProductName��5C      $��2��6L   $��1��6Y   $	��0,2q�6i   ��6.   �� *ProductName+6i   �� 5D   
   -	   5        $ TextBoxDescription��5D      $��3��6L   $��1��6Y   $	��0,2q�6i   $��Stretch=�6w   ��6.   �� *Description+6w   �� 5E   
   -
   5         ��Name$ ComboProductType��5E      $��4��5F      $��1��6    	 �� DisplayMemberPath$	 ProductTypeName��61   
 �� SelectedValuePath$
 ProductTypeId��5G       �� SelectedItem 5E   .   �� *ProductType+5G      �� 5K   
   -   5        $ ComboWorkshop��5K      $��7��5L      $��1��6    $	 WorkshopName��61   $
 
WorkshopId��5M       5K   +   �� *
Workshop+5M      $  Xceed.Wpf.Toolkit.DoubleUpDown  5O   
   -   5          Name$ UpDownPrice��5O      $��8��6P   $��1��6]   $	��100��6m   $
��Left=�6y   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 61   �� *MinimalPrice+6y     5P   
   -   5        $ UpDownPeopleCount��5P      $��9��6W   $��1��6d   $	��100��6t   $
��Left=�6�    69   �� *PeopleCount+6�     5Q   
   -   5        $ UpDownProductionTime��5Q      $��10��6]   $��1��6k   $	��100��6{   $
��Left=�6�    6<   �� *ProductionTime+6�   �� 5R   
   -   5         ��Name$ 
ImagePhoto��5R      $��0��6@   $��1��6M   $��10 0 0 0q�6]   $��Stretch=�5S      $
��Left=�6,   ��5R   $   �� *
GetPhoto+5S   ,   �� 5T   
   -   5        $ BtnLoad��5T      $��0��6"   $��0��6/   $��5q�6?     MaterialDesignFlatButton#���  6K   $��Right=�5U      $	��Top=�6-   .��5W      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ FileUploadOutline=�6&   5X      &   System.Windows.Controls.DataGrid  5[   
   -   5          Name$ DtData��5[         AutoGenerateColumns .  $��11��6@   $��1��6N      
IsReadOnly . $��0 0 50 0q�6p      SelectionMode$ Single =�5]         Columns 5^      4  .System.Windows.Controls.DataGridTemplateColumn  5_      ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$	 100	 6)      Header$ Изображение��65      CellTemplate 5`      �� 5a      .R�5b      �� ��6$   ��  �� Path$ Material.GetPhoto�5c      5e      0
  *System.Windows.Controls.DataGridTextColumn
  5f      $ Материал��6%   $ *	 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Material.MaterialName+6a   
  5g      $ Количество��6%   $ *	 6S    69   �� *Count+6S   5j      �� 5k   
   $��11��6   $��1��6#   $ Vertical=�63   $	��Top=�6J   $��Right=�6b   .+�5l      �� -   5        $ btnAdd��5l      #���  5m   +   ��5l   %   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$	 Add=�5m   +   �� 5n      -   5        $ 	btnDelete��5n      #���  6Y   ��6(     $ Delete=�6�   �� 5o      -   5        $ 	btnChange��5o      #���  6W   ��6(     $
 Edit=�6�   5p      5q      5r      