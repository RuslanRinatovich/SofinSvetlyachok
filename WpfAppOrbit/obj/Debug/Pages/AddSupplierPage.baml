   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfAppOrbit.PagesWpfAppOrbit.Pages  5      '    !WpfAppOrbit.Pages.AddSupplierPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   +localclr-namespace:WpfAppOrbit.Pages  5        N� Title$:  4Добавление и редактирование��5         	base_page#��[  6*   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      .+�5   
   �� $��2��6   $��2��6!   �� 5   
   $��2��6   $��0��6!   $)��#Дата начала работы:��61   $
��20 0q�6M   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DatePickerWorkDate��5      $��2��63   $��1��6@   $
��20,2q�6r   $	��120��6�   $
��Left=�6�      SelectedDate 6P   �� *
WorkDate+6�   �� 5   
   -   5         ��Name$ TextBoxGoodId��5      $��2��6A   $��1��6N   $	��0,2q�6^    �� 
IsReadOnly . $y�	Collapsed=�6~   ��6)   �� *GoodId+6~   �� 5    
   $��Рейтинг, %��6   $��3��6'   $��0��64   $��Center=�6E   $��	20,7,20,6q�6`   %  Xceed.Wpf.Toolkit.IntegerUpDown  5!   
   -   5          Name$ 
UpDownRate��5!      $	��100��5"   5   $
��Left=�5#   4   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	Increment$ 1X�6O   $��20 0 0 0q�6]   $��3��5$   K   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Minimum$	 0X�6X   
   Maximum$	
 100X�6d   $��1��5%   4   $	��0,2q�6D      Value 5$   4   �� *Rate+5%   D   �� 5'   
   -   5         ��Name$ 
ImagePhoto��5'      $��4��6@   $��1��6M   $��Stretch=�5(      $��Stretch=�6,   ��5'   $   �� *
GetPhoto+5(   ,   �� 5*   
   -   5         ��Name$ BtnSave��5*      $��5��6"   $��Сохранить��6/   $��Center=�6D   $��140,5,340,0q�6a   $	��Top=�6v   $	��120��6�   $��1��5+   &   �� 5,   
   $��0��6   $��Название:��6$   $
��20 0q�66   �� 5-   
   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Название��60   $��20 5 0 0q�6[   $��1��6n   $��Center=�6~   $��0��6�   $��30��6�   $	��0,7q�6�   ��6   �� *SupplierName+6�   �� 5.   
   $ ИНН��6'   $��20 5 0 0q�6M   $��1��6`   $��Center=�6p   $��1��6�   $��30��6�   $	��0,7q�6�   ��6   �� *INN+6�   �� 5/   
   $��0��6   $��ИНН:��6$   $��1��60   $
��20 0q�6>   �� 50   
   $��4��6   $��0��6!   $!��Тип поставщика��61   $��	20,3,20,0q�6H   �� 51   
   -   5         ��Name$ ComboSupplierType��51      $��4��52      $��1��6    �� DisplayMemberPath$ SupplierTypeName��6+   $��Stretch=�6P   $��20 0 0 0q�6l    �� SelectedValuePath$ SupplierTypeId��53       �� SelectedItem 51   /   �� *SupplierType+53      54      55      