   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WpfAppOrbit.WindowsWpfAppOrbit.Windows  5   	   '    !WpfAppOrbit.Windows.AddSellWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WpfAppOrbit.Windows     5   	     � Title$  Продажа��5      $	��200��6   $	��600��6(    � 
ResizeMode$ NoResize=�5	         base_window_1#��[  5
       � WindowStartupLocation$ CenterScreen=�5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $	��187 �6   �� 5      .+�5   
   �� $��	Дата ��6   $��Center=�6!   $��1��6<   $��20 0 0 0q�5      $��0��6   $��20��6%   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DatePickerDate��5      $
��Left=�5      $��1��5      $��1��6   $	��240��5      $��2��6      SelectedDate 5      �� *Date+ �� StringFormat$ d�� �� ConverterCulture$ ru-RU��5      �� 5    
   $��0��6   $��0��6!   $��Количество��5!      $��20 0 0 0q�6   $��Center=�5"      �� 5#   
   -   5        	 ��Name$	 TextBoxCount��5#      $��1��5$      $��Center=�6   $��0��5%      $��30��6   $	��0,7q�6   ��5#   (   �� *Count+5%      �� 5&   
   $��2��6   $��0��6!   $��Продукт��61   $��20 0 0 0q�5'      $��Center=�6   �� 5)   
   -   5        
 ��Name$
 ComboBoxProduct��5)       �� IsTextSearchEnabled .  �� 
IsEditable . $��2��5+      $��1��6    �� DisplayMemberPath$ ProductName��5,       �� SelectedValuePath$ 	ProductId��5-       �� SelectedItem 5)   -   �� *	Product+ �� Text 5-   "   �� *Text+�� 5.   
   $��Center=�6    �� Orientation$ 
Horizontal=�5/      $��6��6   $��2��6)   $	��162��50      .+�51      �� -   5         �� 	IsDefault . $��60��51   '   $��5q�65   $
��ОК��6U   �� 52       �� IsCancel . $��60��6%   $��5q�63   $��Отмена��6>   53      54      55      