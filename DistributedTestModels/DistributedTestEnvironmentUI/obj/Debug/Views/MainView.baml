   M S B A M L   `   `   `  ����W  SDistributedTestEnvironmentUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DistributedTestEnvironmentUIDistributedTestEnvironmentUI  5   	   D @DTEModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullF1clr-namespace:DTEModels.Models;assembly=DTEModelsDTEModels.Models 5   	   1    +DistributedTestEnvironmentUI.Views.MainView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8l*clr-namespace:DistributedTestEnvironmentUI     5   	   Clocal1clr-namespace:DTEModels.Models;assembly=DTEModels     5   	     � Title$"  Distributed Test Environment��5   	   $	��615��6.   $	��745��6;    � WindowStyle$ ThreeDBorderWindow=�6G   $��2q�6h   ���	#FF2612EE
���   � � 	Resources 5       �� DataType(  "DTEModels.Models.ComputerNodeModel �� ItemsSource �� Path.  (DTEModels.Models.DistributedProcessModel��%  (��       5   #   *, +)(�� m     5   #   *, +)�� 5   
   "  6#    6O   �� $ ProcessList�.R�5	      �� ��6   �� *
NodeName+5
      �� 5   
   "  6#   .R�5      �� $��5q�65   ��6   �� *ProcessName+65   5      5      ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFB2B5F0��6   	 � Offset$	 0Q�6-   � 5      $ White��6   $	 1Q�6)   5      
 J� Effect
 5      3  -System.Windows.Media.Effects.DropShadowEffect  5   
   .��5      ]� .+�5   
   o� $	��Top=�6   $��20��6$   .@�5      m� $
��Node��6   .@�5      m� $	��Add��6    m� Command 6)   �� *
AddNodeCmd+m� 5      $��Add External��6    62   �� *
AddNodeCmd+m� 5       $��Remove��6    6,   �� *RemoveNodeCmd+5!      m� 5"      $��Process��6   .@�5#      m� $	��Add��6    6)   �� *AddProcessCmd+m� 5$      $��Add External��6    62   �� *AddProcessCmd+m� 5%      $��Remove��6    6,   �� *RemoveProcessCmd+5&      5*      �� 5-   
   $��Bottom=�6   $	��175��6-   .@�5.      �� $	��Log��6   .��5/      &   System.Windows.Controls.DataGrid     AutoGenerateColumns . $
��Left=�6Z   $	��Top=�6v   ��6   �� *LogDataGrid+6v   51      52      �� 54   
    �� Orientation$ Vertical=�6   $
��Left=�6,   $	��200��6B   .+�55      (� .��56      R� -   5        $	��200��56      $��Bottom=�6'   $	��370��6B   ��57      �� *TestEnvironment.ProcessNodes+6C   58      59      (� 5:   
   .��5;      � $��0,0,0,5q�6   .+�5<      ��  �� TextWrapping$
 Wrap=�6    �� 
IsReadOnly . $��130,27,261,341q�6s   ��6@   �� $" CurrentProcess.FrameworkHost�6s   �� 5=      $
��Left=�6   $��	39,27,0,0q�67   $
��Wrap=�6J   $��	Host Name��6^   $	��Top=�6o   $��73��6�   �� 5>       . $
 Wrap=�6,   $��130,51,261,318q�6q   ��6@   �� $  CurrentProcess.ProcessName�6q   �� 5?       . $
 Wrap=�6,   $��130,73,261,295q�6s   ��6@   �� $" CurrentProcess.FrameworkPort�6s   �� 5A      $
��Left=�6   $��	39,50,0,0q�67   $
��Wrap=�6J   $��Process Name��6^   $	��Top=�6r   $��86��6�   �� 5B      $
��Left=�6   $��	38,73,0,0q�67   $
��Wrap=�6J   $��Port Number��6^   $	��Top=�6q   $��74��6�   �� 5C      $��Start��6   $��70��6)   $��300,27,167,333q�64   ��6L   �� *StartProcessCmd+5E      �� 5G      $
��Stop��6   $��70��6(   $��300,65,167,295q�63   ��6K   �� *StopProcessCmd+�� 5I      $��Fault Injection��6   $
��Left=�64   $��78��6O   $��
10,112,0,0q�6[   $	��Top=�6o   $	��472��6�   5K      �� 5L      $��Delay��6   $��111,135,374,233q�6+    d� IsThreeState .  ���	#FF240FF7 d� 	IsChecked 6Y   �� $0 *CurrentProcess.Routing.Faults.DelayMessage� �� Mode$ TwoWay=�6�   �� 5M      $
 Wrap=�6   $��168,135,317,236q�6k   ��6.   �� $, &CurrentProcess.Routing.Faults.Delay_ms�6k   �� 5N      $��	Duplicate��6   $��25,159,435,208q�6/    .  ���	#FF240FF7 6\   �� $4 .CurrentProcess.Routing.Faults.DuplicateMessage�$ TwoWay=�6�   �� 5O      $��Corrupt��6   $��238,135,229,237q�6-    .  ���	#FF240FF7 6[   �� $2 ,CurrentProcess.Routing.Faults.CorruptMessage�$ TwoWay=�6�   �� 5P      $
��Lose��6   $��25,135,462,230q�6*    .  ���	#FF240FF7 6W   �� $/ )CurrentProcess.Routing.Faults.LoseMessage�$ TwoWay=�6�   �� 5Q      $��Out of Order��6   $��111,159,334,213q�62    .  ���	#FF240FF7 6`   �� $7 1CurrentProcess.Routing.Faults.ReverseOrderMessage�$ TwoWay=�6�   �� 5R      $��Process��6   $
��Left=�6,   $��97��6G   $��	10,10,0,0q�6S   $	��Top=�6f   $	��472��6~   �� 5T      $��
Statistics��6   $
��Left=�6/   $	��182��6J   $��
10,197,0,0q�6W   $	��Top=�6k   $	��472��6�   �� 5U      $
 Wrap=�6    . $��88,223,394,145q�6�   ��6@   �� $3 -CurrentProcess.Routing.Stats.MessagesReceived�6�   �� 5V      $
��Left=�6   $��
27,225,0,0q�67   $
��Wrap=�6K   $��Messages��6_   $	��Top=�6o   $��54��6�   �� 5W      $
 Wrap=�6    . $��210,223,274,145q�6�   ��6@   �� $0 *CurrentProcess.Routing.Stats.BytesReceived�6�   �� 5X      $
��Left=�6   $��168,225,0,0q�67   $
��Wrap=�6L   $��Bytes��6`   $	��Top=�6m   $��54��6�   �� 5Y      $
 Wrap=�6    . $��412,223,72,145q�6�   ��6@   �� $5 /CurrentProcess.Routing.Stats.AverageMessageSize�6�   �� 5Z      $
��Left=�6   $��327,225,0,0q�67   $
��Wrap=�6L   $��Average Bytes��6`   $	��Top=�6u   $��84��6�     5[       . $
��Left=�6w   $	��Top=�6�    J� RenderTransformOrigin$ 5.287,21.412&�6�   $	��105��6�   $	��440��6�   $��
25,257,0,0q�6�   ��6   �� **(CurrentProcess.Routing.Stats.MsgDataGrid+6�   5]      5_      5`      5a      