   M S B A M L   `   `   `  ����F  BWpfAppOrbit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfAppOrbitWpfAppOrbit  5   	       WpfAppOrbit.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   %localclr-namespace:WpfAppOrbit  5   	   -   5           base_window#��[  5
      $	��450��5      $	��800��6     � Icon$/  )/WpfAppOrbit;component/Resources/LOGO.ico��6    � Title 5	      ��  �� ElementName$ 	MainFrame�� �� Path$ Content.Title�.��5      �  � RowDefinitions 5   
   �� 5      $��75 �6   �� 5      $��* �6   �� 5      $��30 �6   .+�5   
   �   main#��[ 6   �� 5   
   $/��)/WpfAppOrbit;component/Resources/LOGO.png��6   $��10q�5      $
��Left=�5      �� 5   
   $��0��6   $��СВЕТЛЯЧОК��6!   $��Center=�5      $��Center=�5       
 akcent#��[ 5      #��[ 5      $��20(�5      $
��Bold"�5       � 5   
   -   5          �Name$ 	MainFrame��5      $��1��6#   #��[ 5         � NavigationUIVisibility$ Hidden=�5!      $��1q�6'   6;   �� 5#   
   $��0��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5&      �� -   5         ��Name$ BtnEditProducts��5&        MaterialDesignFlatButton#��[ 6/   $y�	Collapsed=�6a   	 � ToolTip$	 Продукция��5(      $
��Bold"�6G   ��5'      0  *MaterialDesignThemes.Wpf.PackIconExtension  
   Kind$
 	Lightbulb=�5(   G   �� 5)      -   5        $ BtnEditAgents��5)      #��[ 6-   $y�	Collapsed=�6_   $	 Агенты��5+      $
��Bold"�6B   ��5*        $
 
EmojiAgent=�5+   B   �� 5,      -   5        $ BtnEditMaterials��5,      #��[ 61   $	 Материалы��5-   G   $
��Bold"�5.      ��5-        $
 Material=�5.      �� 50      -   5        $ BtnEditSuppliers��50      #��[ 60   $	 Поставщики��51   B   ��6     $	
 Car=�6W   �� 52      -   5        $ BtnBack��52      #��[ 6'   $	 
Назад��53   H   $
��Bold"�54   *   ��53        $
 	ArrowBack=�54   *   55      � 56   
   $��2��6   #��[ 6   57      58      