   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 $   MahApps.Metro.Styles.ProgressBar  �� 
TargetType �� Property  
GrayBrush5 �� Value  ControlBorderBrush  HighlightBrush �� 
TargetType �Name ��Name ��Name  ProgressBrush J� Opacity J� RenderTransformOrigin	 � MappingMode
 � SpreadMethod �� 
StartPoint �� EndPoint �� 	Transform T� X �� AngleX � Offset  ProgressIndeterminateColor1 � Color  ProgressIndeterminateColor2  ProgressIndeterminateColor3  ProgressIndeterminateColor4'    !System.Windows.VisualStateManager    VisualStateGroups%   System.Windows.VisualStateGroup   States    System.Windows.VisualState   
Storyboard g� RepeatBehavior �� 
TargetName �� TargetProperty g� Duration R� KeyTime R� Value  System.Windows.Visibility O� KeyTime O� Value �� Triggers P� Property P� Value  �� 
TargetName! �� Angle 	 MetroProgressBar" �� BasedOn�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %$  &	        &		 �    7   �� 5      "  �65   .�5   
   ��  � ��# �  6'   �� 5   
    � ��# �  6(   �� 5   
    � ��$ 1q�6,   �� 5	   
    � ��# �  6'   �� 5
   
    � �� .  6&   �� 5   
    �@�Maximum$	 100Q�6$   �� 5   
    � ��$ 10��6&   �� 5   
    � ��$ 10��6%   �� 5   
    � �� 5      �� 5      " �6"   .R�5      � $
 Root��6   .+�5      �� $ 
PART_Track��6!   #��z��5   !   #��z��5   !   #��z��5   !   � 5      $ ProgressBarRootGrid��6   .+�5      � $ IndeterminateRoot��6#   $y�	Collapsed=�6>   .+�5   "   �� $ IndeterminateSolidFill��6,   #��z��5   ,   #���  5   ,   $ 1Q�5   ,   $ 0.5,0.5&�5   ,   $��0��5   ,   �� 5   "   $ IndeterminateGradientFill��6,   #��z��5   ,   $	 0.7Q�5    ,   $��1��5!   ,   ��5"   &   �� 5#   *   $	 Absolute=�6>   $
 Repeat=�6U   $
 20,1&�6k   $	 0,1&�6}    5$   .   V� 5%   2   .~�5&   6   T� 
xTransform��$ 0Q�6]   �� 5'   6   $	 -30Q�6D   5(   3   .��5*   .   � $ 0Q�6;   # �  6F   � 5+   .   $ 0.651Q�6;   # �  6J   � 5,   .   $ 0.093Q�6;   # �  6J   � 5-   .   $ 0.548Q�6;   # �  6J   5.   +   50   #   51      � 52      $ DeterminateRoot��6#   $��1q�6<   .+�53   "   �� $ PART_Indicator��6)   $��-1q�54   )   $
��Left=�55   )   #���  56   )   ._�57   &   �� $ GradientFill��60   $	 0.7Q�58   0   $y�	Collapsed=�59   0   ��5:   *   �� 5;   .   $	 Absolute=�6B   $
 Repeat=�6Y   $
 20,1&�6o   $	 0,1&�6�    5<   2   V� 5=   6   .~�5>   :   T� $ 0Q�6M   �� 5?   :   $	 -30Q�6H   5@   7   .��5B   2   � $ 0Q�6?   # �  6J   � 5C   2   $ 0.651Q�6?   # �  6N   � 5D   2   $ 0.093Q�6?   # �  6N   � 5E   2   $ 0.548Q�6?   # �  6N   5F   /   5H   '   5I   #   5J      5K       5L        5M      CommonStates��. 5N   "     Determinate��6.     5O   "   Indeterminate��. 5P   &   �� $ Forever��61   .��5Q   *   T� $ IndeterminateRoot��6H   $ (UIElement.Visibility)�5R   H   $ 00:00:00D�5S   H   ..�5T   .   i� $ 00:00:00��6E    5U   2    5V   6   Visible=�6J   5X   /   5Y   +   T� 5Z   *   $ DeterminateRoot��6H   $ (UIElement.Visibility)�5[   H   $ 00:00:00D�5\   H   ..�5]   .   i� $ 00:00:00��6E    5^   2    5_   6   	Collapsed=�6L   5a   /   5b   +   U� 5c   *   $ 
xTransform��6H   $ X�6k   .\�5d   .   �� $ 0��6C   $ 0Q�6O   �� 5e   .   $ 	00:00:.35��6C   $ 20Q�6W   5f   +   5g   '   5h   #   5i      5k       5l      P� 5m       �@�Orientation$ Vertical=�69   . �5n      �� $
  Root��6%    �@�LayoutTransform 5o   "   �� 5p   &   $	! -90Q�66   5r      5s      P� 5t       �@�IsIndeterminate . . �5u      �� $  DeterminateRoot��6%    � y�$ 	Collapsed=�6X   �� 5v      $  IndeterminateRoot��6%    � y�$ Visible=�6Z   5w      5y      5{      5|      �� 5      8"   "  �5�      5�      