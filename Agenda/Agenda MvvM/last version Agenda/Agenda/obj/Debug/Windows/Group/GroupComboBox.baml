   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Agenda.Windows.GroupAgenda.Windows.Group  5   	   (    "Agenda.Windows.Group.GroupComboBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Agenda.Windows.Group     5   	     � Title$  GroupComboBox��5   	   $	��300��6   $	��300��6,   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      .+�5   
   � .+�5      �� $��0��6   $��30(�6D    �� TextAlignment$
 Left=�6R   $
��Wrap=�6q   $
��Left=�6�   $	��Top=�6�   $
��Auto��6�   $��0,0,0,0q�6�   ��6%   �� *Concatenation+6�   5      �� 5   
   $��1��6   ��6    �� *
AllGroup+ �� SelectedItem 6A   �� *SelectedGroup+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!    � DataContext 6:   �� *DataContext+.+�5      �� $��16��6@   $��16��6K   $��0,2,5,2q�6W   ��6$   �� *
GroupColor+6W   �� 5      $��15(�6:   ��6$   �� *Name+6:   5      5      5      �� 5   
   $��2��6   $��valider��6      ValiderStyle#��[  6T   $��Bottom=�6z   $
��Left=�6�   ��60   �� *ConfirmCommand+6�   �� 5   
   $��2��6   $��Cancel��6     WarningStyle#��[ 6R   $��Bottom=�6x   $��Right=�6�   ��6/   �� *CancelCommand+6�   5      5      