   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfAppOrbit.PagesWpfAppOrbit.Pages  5      (    "WpfAppOrbit.Pages.AddMaterialPage1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f43xtk)http://schemas.xceed.com/wpf/xaml/toolkit  5      +localclr-namespace:WpfAppOrbit.Pages  5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  65     N� Title$:  4Добавление и редактирование��5         	base_page#��[  6)   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��30 �6   �� 5      $��80 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      �� 5      �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      .+�5   
   �� $��0��6   $��2��6!   �� 5"   
   -   5         ��Name$ BtnSave��5"      $��11��6"   $��Сохранить��60   $��Center=�6E   $��140,5,340,0q�6b   $	��Top=�6w   $	��120��6�   $��1��5#   &   �� 5%   
   $��0��6   $��0��6"   $��Изображение��62   $
��20 0q�6F   �� 5&   
   $��1��6   $��0��6!   $��Название��61   $
��20 0q�6B   �� 5'   
   $��2��6   $��0��6!   $��Описание��61   $
��20 0q�6B   �� 5(   
   $��3��6   $��0��6!   $��Тип материала��61   $
��20 0q�6G   �� 5)   
   $��4��6   $��0��6!   $'��!Единица измерения��61   $
��20 0q�6K   �� 5*   
   $��5��6   $��0��6!   $.��(Количество в упаковке��61   $
��20 0q�6O   �� 5+   
   $��6��6   $��0��6!   $1��+Минимальное количество��61   $
��20 0q�6P   �� 5-   
   $��7��6   $��0��6!   $��Стоимость��61   $
��20 0q�6C   �� 5.   
   $��8��6   $��0��6!   $,��&Количество на складе��61   $
��20 0q�6N   �� 5/   
   $��9��6   $��0��6!   $&�� История поставок:��61   $
��20 0q�50      $��30��6"   $	��Top=�6.   �� 52   
   $��10��6   $��0��6"   $&�� История списания:��62   $
��20 0q�53      $��30��6"   $	��Top=�6.   �� 56   
   -   5         ��Name$ TextBoxMaterialName��56      $��1��6N   $��1��6[   $	��0,2q�6k   ��6/   �� *MaterialName+6k   �� 57   
   -   5        $ TextBoxDescription��57      $��2��6L   $��1��6Y   $	��0,2q�6i   $��Stretch=�6w   ��6.   �� *Description+6w   �� 58   
   -   5         ��Name$ ComboMaterialType��58      $��3��59      $��1��6     �� DisplayMemberPath$ MaterialTypeName��61    �� SelectedValuePath$ MaterialTypeId��5:       �� SelectedItem 58   /   �� *MaterialType+5:      �� 5;   
   -   5        $ ComboUnitType��5;      $��4��5<      $��1��6    $ UnitTypeName��61   $ 
UnitTypeId��5=       5;   +   �� *
UnitType+5=      %  Xceed.Wpf.Toolkit.IntegerUpDown  5>   
   -   5        	  Name$	 UpDownCountInPack��5>      $��5��6W   $��1��6d   $	��100��6t   $
��Left=�6�   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Value
 69   �� *CountInPack+6�     5?   
   -   5        $	 UpDownMinimalCount��5?      $��6��6Y   $��1��6f   $	��100��6v   $
��Left=�6�   
 6:   �� *MinimalCount+6�   $  Xceed.Wpf.Toolkit.DoubleUpDown  5@   
   -   5          Name$ UpDownPrice��5@      $��7��6I   $��1��6V   $	��100��6f   $
��Left=�6r   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 61   �� *Price+6r     5A   
   -	   5        $	 UpDownCount��5A      $��8��6K   $��1��6X   $	��100��6h   $
��Left=�6t   
 63   �� *Count+6t   �� 5C   
   -
   5         ��Name$ 
ImagePhoto��5C      $��0��6@   $��1��6M   $��10 0 0 0q�6]   $��Stretch=�5D      $
��Left=�6,   ��5C   $   �� *
GetPhoto+5D   ,   �� 5E   
   -   5        $ BtnLoad��5E      $��0��6"   $��0��6/   $��5q�6?     MaterialDesignFlatButton#���  6J   $��Right=�5F      $	��Top=�6-   .��5H      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ FileUploadOutline=�6&   5I      &   System.Windows.Controls.DataGrid  5K   
   -   5          Name$ DtData��5K         AutoGenerateColumns .  $��9��6@   $��1��6M      
IsReadOnly . $��0 0 50 0q�6o      SelectionMode$ Single =�5M         Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header$ Код поставки��6%   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6;   �� *MaterialSupplierId+  5P      $ Поставщик��6%      Width5
  /System.Windows.Controls.DataGridLengthConverter$ *
 6b    68   �� *Supplier.SupplierName+6b     5Q      $ Количество��6%   $ *
 6S    69   �� *Count+6S     5R      $ 	Дата ��6%    64   �� *DeliveryDate+ �� StringFormat$ d�� �� ConverterCulture$ ru-RU��4  .System.Windows.Controls.DataGridTemplateColumn  5S      $ *
 6)   $ Качество��63      CellTemplate 5T      �� 5U      .R�5V      (  "MaterialDesignThemes.Wpf.RatingBar    Name$ ReadOnlyRatingBar��68      
IsReadOnly .    Value 6T   �� *	GetRate+6o   5W      5Y      5[      �� 5\   
   $��9��6   $��1��6"    �� Orientation$ Vertical=�62   $	��Top=�6I   $��Right=�6a   .+�5]      �� -   5        $ btnAdd��5]      #���  6h   ��6%   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$	 Add=�6h   �� 5^      -   5        $ 	btnDelete��5^      #���  6q   ��6(     $ Delete=�6q   �� 5_      -   5        $ 	btnChange��5_      #���  6o   ��6(     $
 Edit=�6o   5`        5d   
   -   5        $ DtDataWriteOff��5d       .  $��10��6H   $��1��6V    . $��0 0 50 0q�6x   $ Single =�5f       5g        5h      $ Код��6%    62   �� *MaterialSupplierId+  5i      $ Причина��6%   $ *
 6Q    66   �� *Reason+6Q     5j      $ Количество��6%   $ *
 6S    69   �� *Count+6S     5k      $ 	Дата ��6%    64   �� *OperationDate+$ d��$ ru-RU��5n      �� 5o   
   $��10��6   $��1��6#   $ Vertical=�63   $	��Top=�6J   $��Right=�6b   .+�5p      �� -   5        $ btnAddWriteOff��5p      #���  6x   ��6-     $	 Add=�6x   �� 5q      -   5        $ btnDeleteWriteOff��5q      #���  6�   ��60     $ Delete=�6�   �� 5r      -   5        $ btnChangeWriteOff��5r      #���  6   ��60     $
 Edit=�6   5s      5t      5u      