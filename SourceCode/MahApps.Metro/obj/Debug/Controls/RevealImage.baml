   M S B A M L   `   `   `  ����  MahApps.Metro(    "MahApps.Metro.Controls.RevealImage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          >�Name$  revealImage��5       � 	Resources 5         OnMouseEnter1 �� 
TargetName �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value   EasingFunction/  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  OnMouseLeave1��%�  &	        &	 n    �� 5   
   .��5      U� $ border��6,   $ (UIElement.Opacity)�6K   .\�5        $ 0��6'   $ 0Q�63    5        5      $ EaseOut=�6%   5        5      $ 0:0:0.3��6'   $ 1Q�69   5      U� 5      $ 	textBlock��6,   $ (UIElement.Opacity)�6N   .\�5        $ 0:0:0.0��6'   $ 1Q�69   5      5      �� 5   
   .��5      U� $ 	textBlock��6,   $ (UIElement.Opacity)�6N   .\�5        $ 0:0:0.2��6'   $ 0Q�69   5      U� 5      $ border��6,   $ (UIElement.Opacity)�6K   .\�5        $ 0:0:0.2��6'   $ 0Q�69   5      5       5!       � Triggers 5#      :� 5$   
   	 :� RoutedEvent$	 Mouse.MouseEnter��6   
 :� 
SourceName$

 grid��66   .[�5%      �� #��[  6   5&      :� 5'   
   $	 Mouse.MouseLeave��6   $

 grid��66   .[�5(      �� -   5         ��Name$# OnMouseLeave1_BeginStoryboard��5(      #��[ 6E   5)      .��5,      � -   5         �Name$
 grid��5,      .+�5-   
   �� $��0q�6   $
��Left=�5.      $	��Top=�5/      $
��Fill=�51      ��50      ��  �� ElementName$ revealImage�� �� Path$ Image�51      �� 52   
   -   5         ��Name$ border��52      
�������$��2q�54       J� Opacity$ 0Q�55      ._�56      �� $��0q�6   
���   �$��0��58      ��59      
� 5:       
� Center$ 	0.25,0.25&�6*    
� GradientOrigin$ 	0.25,0.25&�6=    �� Opacity$ 0.615Q�6X   .��5;      �  � Offset$ 1Q�6'    � Color$ Black��62   � 5<      $ 	#FF8D8D8D��6'   5=      5?      5@      �� 5A   
   -   5         ��Name$ 	textBlock��5A      $��5q�5B      $��Bottom=�5C      $��Segoe WP SemiLight)�5D      
�������$ 0Q�5F      $
��Wrap=�5G      5H      5I      