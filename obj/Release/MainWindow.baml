   M S B A M L   `   `   `  ����   ColorBasics-WPF5    /Microsoft.Samples.Kinect.ColorBasics.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Color Basics��$	��769��$	��851�� � 	Resources    MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property �� Value  SnapshotButton �� 
TargetType �� Orientation ��Name	 �� TextAlignment
 ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	      &	 *     '�� ?     &	 t     7  7  �� $ 	#ff6e6e6e���� $ 	#ff52318f���� $ 	#ff00BCF2���� " ��.���  �@J�SnapsToDevicePixels . �� " ��.���  � �� �� " ��.R�� .+��� $ 
Horizontal=�
������ .+��� $ SnapText��#��z��$
	 Left=�$��Center=�8��  $��15(�� $��9,0,0,0q�.+��� $
 
SnapNormal��$��Images\SnapNormal.png��$
��None=�$��Center=��� $
 	SnapHover��$��Images\SnapHover.png��$
��None=�$��Center=�$y�	Collapsed=� P�  �@J�IsMouseOver . . ��� $ 
SnapNormal�� � y�$ 	Collapsed=��� $ 	SnapHover�� � y�$ Visible=��� 8  $ SnapText�� � ��.��� -   $��
layoutGrid��$��10,0,10,-21q� � RowDefinitions �� $
��Auto �$��68���� $	��73* ��� $
��271* ��� $
��253* ��� $
��Auto �$��66���� $��25 ��� $��0 �.+�]� $��0��$��0 0 0 20q�.+��� $��Right=�$��0 0 -1 0q�$��Bottom=�#��[  $��Segoe UI)�$��18(��� $�Práctica 1���� �� (� $��1�� (� Stretch$ Uniform=�$��Center=�$��3��$��	44,0,43,0q�$	��736��.���� -   $��Image��$	��640��$	��480�� J� RenderTransformOrigin$ 0.567,0.502&��� -   $��	statusBar��$��4��$��Stretch=�$��Bottom=�
�������#��[  $��26��$��248,0,137,0q�.@��� $��0 0 0 10q�$	��312��.���� -   $��statusBarText��$��-1 0 0 0q�]� $
��Left=���.  $��3��$��Stretch=�$	��142��$��1��$��	0,0,0,190q�.+��� -   $	
 pi1��$��Images\posini.png��$
��None=�$��Center=�$��Center=�$��20,-110,0,5q�$y�Hidden=�$��98���� -   $

 mov1��$��Images\mov1.png��$
��None=�$
��Left=�$��Center=�$��-100,-10,0,5q�$y�Hidden=�$	��138���� -   $

 mov2��$��Images\mov2.png��$
��None=�$
��Left=�$��Center=�$��-140,-110,0,5q�$y�Hidden=�$	��138���� -	   $ 
textoAyuda��$
��Left=�$��-100 0 -1 0q�$	��Top=�$
��Left=�#��[  $��Segoe UI)�$��18(�$	��150��$ 	-0.34,5.5&��� $� ���� -
    ��Name$ mError�� � Value$ 15Q�$
��Left=�$��	10,41,0,0q�$��4��$	��Top=�$	��217�� � Maximum$ 25Q� � Minimum$ 5Q�$��18���� -   $ 	textBlock��$
��Left=�$��37��$��10,3,0,0q�$��4��$
��Wrap=�$	��Top=�$	��217���� $�Margen de error���� $� (m):���� �� $�0.05		0.15		0.25	��