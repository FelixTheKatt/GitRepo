   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Agenda.Windows.GroupAgenda.Windows.Group  5   	   &     Agenda.Windows.Group.GroupWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Agenda.Windows.Group     5   	     � WindowStyle$
  None=�5   	    � 
ResizeMode$ CanResizeWithGrip=�6    � AllowsTransparency .  � Title$ GroupWindow��5	   	   $	��350��6   $	��525��6*   .��5
      �    DefaultSelectedItem#��[  6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��2��6   $��0��6$    	 Title#��[ 61   .+�5      �� $��GroupManager��6     LabelMainTitle#��[ 6+   6U   5      �� 5   
   $
��Name��6   $��1��6   $��0��6,     defaultLabel#��[ 6<   6d   �� 5   
     warningLabel#��[ 6Z   $��1��6�   $��1��6�   $	��Top=�6�   ��6   ��  �� Path$ WarningName� �� UpdateSourceTrigger$ PropertyChanged=���6�   �� $ Namelabelcolor�$ PropertyChanged=�6  �� 5   
   $��1��6R   $��1��6_   $��Bottom=�6o     DefaultTextBox#��[ 6�   ��6   �� $
 Name�$ PropertyChanged=�6�   �� 5   
   $��Color��6   #��[ 6    $��2��6F   $��0��6S   �� 5   
   $�� ��6   $��2��6   $��1��6(   $	��Top=�68   6R   �� 5   
   $��2��6   $��1��6      DefaultComboBox#��[ 60   $��Bottom=�6Y   $	��150��6�   ��6t   �� *	ColorList+ �� SelectedItem 6�   �� *SelectedColor+��5      �� 5      .R�5       �� 	 �� Orientation$	 
Horizontal=�6!   
 � DataContext
 6:   �� *DataContext+.+�5!      �� $��16��6A   $��16��6L   $��0,2,5,2q�6X   ��6$   �� *ColorPreset+6X   �� 5"      ��6$   �� *	ColorName+5#      5$      5'      �� 5(   
   $��Confirm��6     ValiderStyle#��[ 6F   $��3��6l   $��0��6y   ��6#   �� *ConfirmCommand+6�   �� 5)   
   $��Cancel��6     WarningStyle#��[ 6D   $��3��6j   $��1��6w   ��6"   �� *CancelCommand+6�   5*      5+      