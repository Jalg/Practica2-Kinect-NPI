   M S B A M L   `   `   `  ����  ColorBasics-WPF5    /Microsoft.Samples.Kinect.ColorBasics.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Color Basics��5      $	��769��6   $	��851��6'    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property �� Value  SnapshotButton �� 
TargetType �� Orientation ��Name	 �� TextAlignment
 ��Name �� Triggers P� Property P� Value �� 
TargetName��%1  &	        &	 #     &	 F     '�� i     &	 �     7  7  �� 5	   
   $ 	#ff6e6e6e��62   �� 5
   
   $ 	#ff52318f��64   �� 5   
   $ 	#ff00BCF2��62   �� 5   
   " ��6   .�5      ��  �@J�SnapsToDevicePixels . 64   5      �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ 
Horizontal=�6)   
������ .+�5   "   �� $ SnapText��6,   #��z��6>   $
	 Left=�6_   $��Center=�6u   8��  $��15(�6�   � 5   "   $��9,0,0,0q�6'   .+�5   &   �� $
 
SnapNormal��6,   $��Images\SnapNormal.png��6@   $
��None=�6_   $��Center=�6n   �� 5   &   $
 	SnapHover��6,   $��Images\SnapHover.png��6?   $
��None=�6]   $��Center=�6l   $y�	Collapsed=�6�   5   #   5      5       5      P� 5       �@J�IsMouseOver . . �5   "   �� $ 
SnapNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5   "   $ 	SnapHover��6O    � y�$ Visible=�6?   6O   �� 5    "   8  $ SnapText��6h    � ��5!      5#      5%      5&      5'      .��5)      � -   5        $��
layoutGrid��5)      $��10,0,10,-21q�6    � RowDefinitions 5*   
   �� 5+      $
��Auto �6   $��68��6*   �� 5,      $	��73* �6   �� 5-      $
��271* �6   �� 5.      $
��253* �6   �� 5/      $
��Auto �6   $��66��6*   �� 50      $��25 �6   �� 51      $��0 �6   .+�53   
   ]� $��0��6   $��0 0 0 20q�6!   .+�54      �� $��Right=�6   $��0 0 -1 0q�6/   $��Bottom=�6A   #��[  6\   $��Segoe UI)�6�   $��18(�6�   �� 6�   $�
Práctica ��6�   �� 6�   $�2��6�   �� 6�   �� 6�   �� 6�   �� 6�   6�   55      (� 56   
   $��1��6    (� Stretch$ Uniform=�6   $��Center=�61   $��3��6N   $��	44,0,43,0q�6_   $	��736��6r   .��57      �� -   5        $��Image��57      $	��640��6!   $	��520��6-    J� RenderTransformOrigin$ 0.567,0.502&�6:   58      �� 59   
   -   5        $��	statusBar��59   ?   $��4��6   $��Stretch=�6!   $��Bottom=�6P   
�������#��[  6~   $��26��6�   $��248,0,137,0q�6�   .@�5:      �� $��0 0 0 10q�6   $	��312��6/   .��5;      �� -   5        $��statusBarText��5;      $��-1 0 0 0q�61   6E   5<      5=      5A      5B      