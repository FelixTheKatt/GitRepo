   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Agenda.Windows.EventAgenda.Windows.Event  5   	   &     Agenda.Windows.Event.EventWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Agenda.Windows.Event     5   	     � WindowStyle$
  None=�5   	    � 
ResizeMode$ CanResizeWithGrip=�6    � AllowsTransparency .  � Title$ EventWindow��5	   	   $	��350��6   $	��525��6*   .��5
      �    DefaultSelectedItem#��[  6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6   $��2��6    	 Title#��[ 60   .+�5      �� -   5        $��
EventTitle��5      $��Add Your Events��6&   $��Center=�6@     LabelMainTitle#��[ 6^   6�   5      �� 5   
   -   5        $��labelEventName��5      $��1��6&   $��0��63   $
��NAME��6C     defaultLabel#��[ 6R   6z   �� 5   
   -   5        $��	CheckName��5      $��1��6!   $��1��6.     warningLabel#��[ 6�   $	��Top=�6�   ��6>   ��  �� Path$ WarningEventName� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5        $��InputEventName��5      $��1��6(   $��1��65   $��Bottom=�6�   ��6E   �� $
 Name�$ PropertyChanged=�6�   �� 5   
   -   5        $��labelEventDate��5      $��2��6&   $��0��63   $
��DATE��6C   #��[ 6R   6z   �� 5   
   $��2��6   $��1��6    $
��Left=�60     DefaultComboBox#��[ 6�   ��6K   �� *	YearsList+ �� SelectedItem 6m   �� *
DateYear+6�   �� 5    
   $��2��6   $��1��6!   $��Center=�61   #��[ 6�   ��6N   �� *
MonthsList+ 6q   �� *	DateMonth+5"      �� 5#   
   $��2��6   $��1��6!   $��Right=�61   #��[ 6�   ��6M   �� *
DaysList+ 6n   �� *	DateDay+5%      �� 5&   
   -   5        $��labelEventTime��5&      $��3��6&   $��0��63   $
��Time��6C   $
��Left=�6R   #��[ 6m   �� 5'   
   -   5        $��LabelQuestionTime��5'      $��3��6)   $��0��66   $��Specify an hour ?��6F   $��Right=�6b   $��Center=�6~   $��Century Gothic)�6�   $��12(�6�   �� 5(   
   $��3��6   $��1��6    $
��Left=�60   $��Center=�6m   	 d� 	IsChecked	 6K   �� *CheckedTime+6m   �� 5)   
   $��3��6   $��1��6   #��[ 6w   $	��Top=�6�   ��6-   �� $ WarningTime�$ PropertyChanged=���6�   �� $ TimeLabelColor�$ PropertyChanged=�6�   �� 5*   
   $��3��6   $��1��6    $��Center=�60   #��[ 6�   $��Bottom=�6�   {�6M   �� *CheckedTime+��6o   �� *	HoursList+ 6�   �� $ 	TimeHours�6�   �� 5+   
   $��3��6   $��1��6    $��Right=�60   #��[ 6�   $��Bottom=�6�   {�6L   �� *CheckedTime+��6n   �� *MinutesList+ 6�   �� $ TimeMinutes�6�   �� 5-   
   -   5        $��labelEventGroup��5-      $��4��6'   $��0��64   $��Group��6D   #��[ 6T   �� 5.   
   $��4��6   $��1��6   #��[ 6x   $	��Top=�6�   ��6-   �� $ WarningGroup�$ PropertyChanged=�6�   �� 5/   
   $��4��6   $��1��6    $
��Left=�60   $��Bottom=�6�   #��[ 6�   $	��125��6�   ��6K   �� *
AllGroup+ 6l   �� $ SelectedGroup���50      �� 51      .R�52      �� 
 �� Orientation$
 
Horizontal=�6!    � DataContext 6:   �� *DataContext+.+�53      �� $��16��6@   $��16��6K   $��0,2,5,2q�6W   ��6$   �� *
GroupColor+6W   �� 54      $��10(�6:   ��6$   �� *Name+6:   55      56      58      �� 5:   
   $��4��6   $��1��6   $��Right=�6.   $��	Add Group��6J     SmallDefaultStyle#��[ 6^   $��Bottom=�6�   ��6�   �� *AddgroupCommand+6�   �� 5<   
   -	   5        $��AddEvent��5<      $��5��6"   $��0��6/   $��Center=�6?   $��	Add Event��6\     ValiderStyle#��[ 6�   ��6p   �� *AddEventCommand+6�   �� 5=   
   -
   5        $��Cancel��5=      $��5��6   $��1��6,   $��Center=�6<   $��Cancel��6Y     WarningStyle#��[ 6�   ��6j   �� *CancelCommand+6�   5>      5@      