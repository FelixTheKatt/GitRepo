   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Agenda.Windows.GroupAgenda.Windows.Group  5   	   -    'Agenda.Windows.Group.GroupDetailsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Agenda.Windows.Group     5   	     � Title$  GroupDetailsWindow��5   	   $	��324��6$   $	��330��61   .��5	      � .+�5
   
   �� $��8q�64   ��6   �� *
GroupColor+._�5      �  � RowDefinitions 5      �� 5      �� 5      $��20 �6$   .+�5      �� $��0��6   ��6(   �� *	EventList+ �� View 5      �� 5      .P�5      �� $
��Name��6)    �� Width$	 100��67    �� DisplayMemberBinding 6C   �� *Name+�� 5      $
��Date��6)   $	 100��68    6D   �� *
DateNoTime+�� 5      $
��Time��6)   $	 100��68    6D   �� *
TimeNoDate+5      5      �� 5      $��1��6   $��toggle History��6&   
���������6?   �� *HistoryCommand+6b   5      5      5      5      