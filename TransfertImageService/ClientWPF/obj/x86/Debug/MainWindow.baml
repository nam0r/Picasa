   M S B A M L   `   `   `  ����D  @ClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientWPF	ClientWPF  5   	       ClientWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   )localclr-namespace:ClientWPF     5   	     � Title$  
MainWindow��5   	   $	��720��6   $	��900��6)    � 	Resources 5      )   #ClientWPF.ByteArrayToImageConverter   ByteArrayToImageConverter  ImageCollection1  ImageCollection2  ImageSourceTemplate �� Padding �� CornerRadius �� Orientation �� Path �� 	Converter �� Mode  ImageListBoxStyle �� 
TargetType	 �� Property
 �� Value��%  &	        &	      &	 &     &	 9     7   &	 �    7    5   
   6*   S� 5   
   6   S� 5	   
   6   �� 5
   
   .R�5      �� $ 5,5,5,5q�6   $��5,5,5,5q�6'   
�������$��3q�5      $ 5��6   ._�5      �� $ Vertical=�6   .+�5      �� $	��100��6   $	��100��6)   $
��Fill=�66   ��5      �� $ Image�8   $ Default=��� 5      $��Center=�5      ��5       �� $	 Nom�5      5      5      5      �� 5   
   " ��6*   .�5      �� 	 �@J�	AllowDrop
 . 6*   �� 5      	 � ��
 6,   �� �� 5      	 � ��8
   6-   �� 5      	 � ��$
 Visible=�6I   �� 5      	 � ��$
 Disabled=�6K   �� 5      	 � ��
 5      �� 5      .R�5       � 5!      5#      5$      5%      .��5'      �  � RowDefinitions 5(   
   �� 5)      $��* �6   �� 5*      $
��Auto �6   �� 5+      $��* �6   .+�5-   
   �� -   5         ��Name$ ListBox1��5-      #��[ 5.       � DataContext# [ 5/      $��	0,0,158,0q�52   (   �� 53   
   $��Stretch=�6    �� ResizeBehavior$ PreviousAndNext=�54      $��5��55      $��1��56      $��	0,0,135,0q�6$   �� 57   
   -   5        $ ListBox2��57      #��[ 58      # [ 59      $��2��5=      �� 5?   
   $��745,57,32,257q�6    �� Target 6'   ��  �� ElementName$ Username��.��6O   _Nom de l'album6`   �� 5@   
   -   5        $��Username��5@   )   $��739,95,21,222q�6   ��69   �� $ User.Username� �� UpdateSourceTrigger$ PropertyChanged=�$ TwoWay=� �� ValidatesOnDataErrors . �� 5A   
   $��739,133,21,184q�6   ��6)   �� $ CreateAlbum���6N   �� .��6k   Créer album6x   5C      5D      