   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfAppOrbit.WindowsWpfAppOrbit.Windows  5   	   1    +WpfAppOrbit.Windows.MaterialSupplierWindow1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -local!clr-namespace:WpfAppOrbit.Windows  5   	   -   5          � Title$:  4Добавление и редактирование��5	   
    � WindowStartupLocation$ CenterScreen=�6.   $	��300��5
   	   $	��600��6      base_window_1#��[  6#   .��5      � -   5         � RowDefinitions 5   
   �� 5      �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $	��187 �6   �� 5      .+�5   
   �� $��Поставщик��6   $��Center=�6%   $��0��6@   $��20 0 0 0q�6M   $��0��6`   $��16��6p   �� 5   
   -   5         ��Name$ ComboSupplier��5      $��0��5      $��1��6     �� DisplayMemberPath$ SupplierName��61    �� SelectedValuePath$ 
SupplierId��5       �� SelectedItem 5   +   �� *
Supplier+5      �� 5   
   $��Дата поставки��6   $��Center=�6)   $��1��6D   $��20 0 0 0q�6Q   $��0��6d   $��16��6t   &   Xceed.Wpf.Toolkit.DateTimePicker  5   
   -   5          Name$ DatePickerDate��5      $
��Left=�6p   $��1��5      $��1��6,   $	��240��6<   $��2��6H   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Value	 5   7   �� *DeliveryDate+
 �� ConverterCulture$
 ru-RU��5   H   �� 5"   
   $��Количество��6   $��Center=�6&   $��2��6A   $��20 0 0 0q�6N   $��0��6a   $��16��6q   %  Xceed.Wpf.Toolkit.IntegerUpDown  5$   
   -   5          Name$ UpDownCount��5$      �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X�62   $��2��5%   5   $��1��6B   $	��100��6R      Maximum$
 1000X�6^   $
��Left=�6m      Value 6   �� *Count+6m   �� 5&   
   $��Качество��6   $��Center=�6$   $��3��6?   $��20 0 0 0q�6L   $��0��6_   $��16��6o     5(   
   -   5        $ UpDownQuality��5(      $ 0X�6P   $	 100X�6\   $��3��6j   $��1��6w   $	��100��6�   $
��Left=�6�    66   �� *	Quality+6�   �� 5*   
   $��Center=�6    �� Orientation$ 
Horizontal=�62   $��10��6K   $��2��6Y   $��127,0,103,0q�6m   $	��162��6�   .+�5+      �� -   5         �� 	IsDefault . $��60��5+   ;   $��5q�6I   .��6T   OK6X   �� 5,       �� IsCancel . $��60��6%   $��5q�63   .��6>   Отмена6F   5-      5.      5/      