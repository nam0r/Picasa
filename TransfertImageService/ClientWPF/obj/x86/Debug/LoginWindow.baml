   M S B A M L   `   `   `  ����D  @ClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:ProjectSoftware.LoginExample.BLL ProjectSoftware.LoginExample.BLL  5      .    (ProjectSoftware.LoginExample.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @local.clr-namespace:ProjectSoftware.LoginExample.BLL     5        � Title$  Login��5      $	��260��6   $	��300��6    .��5      � -   5        $��MainGrid��5      $��5q�6    � 	Resources 5	   
    �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value �� RelativeSource	 �� Path��%e  '��       '�� G     �� 5
      " ��6   .�5      ��  � ��$ 5q�6+   5      �� 5      " ��6   .�5      ��  � ��$ 5q�6+   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      ]� ��. .+�5   "   �� $��Right=�6,   
���  ��$��14(�5   %   $
��Bold"�63   !6H   �� 5   "   
���  ��$��1q�6;   ._�5   &   �� 6B   5   #   5      5      5       5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5       ��  �� *Self+$2	 ,(Validation.Errors).CurrentItem.ErrorContent�5#      5%      5&      
 � RowDefinitions
 5'   
   �� 5(      $��50 �6   6*   �� 5)      $��30 �6   6*   �� 5*      $��30 �6   6*   �� 5+      $��30 �6   6*   �� 5,      $��30 �6   6*   �� 5-      $��30 �6   6*    � ColumnDefinitions 5/   
   �� 50      6!   �� 51      6!   .+�54   
   �� $��2��6   $��0��6)   .+�55      �� $
��Bold"�6   Login62   �� 57      
���  ����6   �� $	 Error� �� Mode$ TwoWay=�6T   59      �� 5:   
   $��1��6   $��1��6     �� Target 6-   ��  �� ElementName$ Username��.��6U   	_Username6`   �� 5;   
   -   5        $��Username��5;   "   $��0��6   $��1��62   ��5<      �� $	 User.Username� �� UpdateSourceTrigger$ PropertyChanged=�$ TwoWay=� �� ValidatesOnDataErrors . �� 5B   
   $��1��6   $��2��6     6-   �� $ Password��.��6U   	_Password6`   �� 5C   
   -   5        $��Password��5C   /   $��0��6   $��2��6"   ��5D      �� $	 User.Password�$ PropertyChanged=�$ TwoWay=� . .��6�   5E      �� 5G   
   $��1��6   $��3��6     6-   �� $
 Path��.��6Q   _Répertoire initial6f   �� 5H   
   -   5        $
��Path��5H   /   $��0��6   $��3��6"   ��5I      �� $
	 Path�$ PropertyChanged=�$ TwoWay=� . .��6}   5J      �� 5L   
   $��0��5N      $��4��6!   ��5L      �� $	 LoginCommand���5M      �� .��5N   .   Login65   �� 5P   
   $��1��5R      $��4��6!   ��5P      �� $	 CancelCommand���5Q      �� .��5R   .   Cancel66   �� 5T   
   $��2��6   $��5��6(   
�������+)A des fins de test, nom et pass valides :�� 5U   8   "username" et "password".5W      5Y      5Z      