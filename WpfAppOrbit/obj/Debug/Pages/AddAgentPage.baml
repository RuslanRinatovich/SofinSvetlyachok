   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfAppOrbit.PagesWpfAppOrbit.Pages  5      $    WpfAppOrbit.Pages.AddAgentPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:WpfAppOrbit.Pages     5      X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f43xtk)http://schemas.xceed.com/wpf/xaml/toolkit  5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$:  4Добавление и редактирование��5   	      	base_page#��[  5      .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5       .+�5"   
   �� $��0��6   $��2��6!   �� 5%   
   -   5         ��Name$ BtnSave��5%      $��11��6"   $��Сохранить��5&      $��Center=�5'      $	��Top=�5(      $	��120��6   $��1��5)      $��2��5*      �� 5,   
   $��0��6   $��0��6"   $��Логотип��62   $
��20 0q�6B   �� 5-   
   $��1��6   $��0��6!   $��Наименование��61   $
��20 0q�6F   �� 5.   
   $��2��6   $��0��6!   $��Тип агента��61   $
��20 0q�6D   �� 5/   
   $��3��6   $��0��6!   $��Приоритет��61   $
��20 0q�6C   �� 50   
   $��4��6   $��0��6!   $��
Адрес��61   $
��20 0q�6?   �� 51   
   $��5��6   $��0��6!   $��ИНН��61   $
��20 0q�6=   �� 52   
   $��6��6   $��0��6!   $��КПП��61   $
��20 0q�6=   �� 53   
   $��7��6   $��0��6!   $��Имя директора��61   $
��20 0q�6G   �� 54   
   $��8��6   $��0��6!   $��Телефон��61   $
��20 0q�6A   �� 55   
   $��9��6   $��0��6!   $��Email��61   $
��20 0q�6?   �� 56   
   $��10��6   $��0��6"   $"��История продаж:��62   $
��20 0q�57      $��30��6   $	��Top=�6   �� 59   
   -   5         ��Name$ 
ImagePhoto��59      $��0��5:      $��1��6   $��10 0 0 0q�6!   $��Stretch=�5;      $
��Left=�6    ��59   $   �� *
GetPhoto+5;       �� 5<   
   -   5        $ BtnLoad��5<      $��0��6"   $��0��6/   $��	120,5,0,0q�6?   $
��Left=�5=      $	��Top=�6     MaterialDesignFlatButton#���  6q   ��65   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ FileUploadOutline=�5>      �� 5@   
   -   5         ��Name$ TextBoxAgentName��5@      $��1��5A      $��1��6   $	��0,2q�6    ��5@   ,   �� *	AgentName+5A       �� 5B   
   -   5         ��Name$ ComboAgentType��5B      $��2��5C      $��1��6    �� DisplayMemberPath$ AgentTypeName��6"   	 �� SelectedValuePath$	 AgentTypeId��5D      
 �� SelectedItem
 5B   ,   �� *	AgentType+5D      %  Xceed.Wpf.Toolkit.IntegerUpDown  5G   
   -   5          Name$ UpDownPriority��5G      $��3��5H   7   $��1��6D   $	��100��6T   $
��Left=�6`   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6   �� *
Priority+6`   �� 5J   
   -   5        $ TextBoxLegalAddress��5J      $��4��5K      $��1��6   $	��0,2q�6    ��5J   /   �� *LegalAddress+5K       �� 5L   
   -   5        $ 
TextBoxINN��5L      $��5��5M      $��1��6   $	��0,2q�6    ��5L   &   �� *INN+5M       �� 5N   
   -	   5        $ 
TextBoxKPP��5N      $��6��5O      $��1��6   $	��0,2q�6    ��5N   &   �� *KPP+5O       �� 5P   
   -
   5        $ TextBoxManagerFIO��5P      $��7��5Q      $��1��6   $	��0,2q�6    ��5P   -   �� *
ManagerFIO+5Q       �� 5R   
   -   5        $ TextBoxPhone��5R      $��8��5S      $��1��6   $	��0,2q�6    ��5R   (   �� *Phone+5S       �� 5T   
   -   5        $ TextBoxEmail��5T      $��9��5U      $��1��6   $	��0,2q�6    ��5T   (   �� *Email+5U       &   System.Windows.Controls.DataGrid  5W   
   -   5          Name$ DtData��5W         AutoGenerateColumns .  $��10��5X      $��1��6      
IsReadOnly . $��0 0 50 0q�5Y         SelectionMode$ Single =�5Z         Columns 5\      0  *System.Windows.Controls.DataGridTextColumn  5]      ,  &System.Windows.Controls.DataGridColumn   Header$ № п��6%      
Visibility$ Hidden=�5^   $   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *SellHistoryId+6$     5_      $ Продукт��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5`   ,    6   �� *Product.ProductName+6,     5a      $ Количество��6%   $ * 5b       6   �� *Count+6     5c      $ Дата продажи��6%    5d      �� *Date+ �� StringFormat$ d�� �� ConverterCulture$ ru-RU��5f      �� 5g   
   $��10��6   $��1��6#    �� Orientation$ Vertical=�63   $	��Top=�5h      $��Right=�6   .+�5i      �� -   5        $ btnAdd��5i      #���  6S   ��6%     $	 Add=�6�   �� 5j      -   5        $ 	btnDelete��5j      #���  6Y   ��6(     $ Delete=�6�   5k      5l      5m      