   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyToDo.ViewsMyToDo.Views  5      C&clr-namespace:MyToDo.Common.ConvertersMyToDo.Common.Converters  5          MyToDo.Views.MemoView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:MyToDo.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.6.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a:behavior+http://schemas.microsoft.com/xaml/behaviors  5      6	converter&clr-namespace:MyToDo.Common.Converters  5        � 	Resources  5      7   1MyToDo.Common.Converters.IntToVisibilityConverter   IntToVisibility��%   &	          5   
   6-   5      .��5      )  #MaterialDesignThemes.Wpf.DialogHost  .��5   
   )  #MaterialDesignThemes.Wpf.DrawerHost     IsRightDrawerOpen 6$   �� *IsRightDrawerOpen+   RightDrawerContent 5      ]� 5      $	��300��6   $��False��5      $
��15,5q�5      .+�5      �� $��添加备忘��6    $��20(�5       $
��Bold"�5       $	��Top=�5       �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入备忘概要��5      $	��Top=�5      $
��0,10q�5      ��5      �� *CurrentDto.Title+5      �� 5      $ 请输入备忘内容��5      $	��100��5        �� TextWrapping$
 Wrap=�5!       �� AcceptsReturn$
 True��5"      $	��Top=�5#      $
��0,10q�5$      ��5      �� *CurrentDto.Content+5$      �� 5%      $��保存备忘��6   $	��Top=�5&      $
��Save��5(      ��5'      �� *ExecuteCommand+5(      5)      .��5,      �  � RowDefinitions 5-      �� 5.      $
��Auto �6$   �� 5/      .+�51      �� $��0��6    �� Orientation$ 
Horizontal=�52      $
��15,5q�53      .+�54      �� $	��250��55      $ 查找备忘事项��56      .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True��57      $��Center=�58      ��54      �� *Search+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�.��58   9    J� InputBindings 59      �� 5:      
 �� Key$ Enter��6)    �� CommandParameter$ Query��5<   )    �� Command 5;   )   �� *ExecuteCommand+5<   )   .��5=   1   5>      5?      �� 5@      $��0��6   $��+ 添加备忘��5A      $	��Add��5C      $��Right=�5D      $
��15,5q�5E      ��5B      �� *ExecuteCommand+5E      �� 5F      $��1��6   $��Center=�5G      y�5H      �� *MemoDtos.Count+ �� 	Converter# [  .+�5I      �� $��../Images/wind.png��6   $	��128��5J      �� 5K      $B��<没有数据，请更换搜索条件或者添加新数据！��6    $��18(�5L       $��Center=�5M       $
��0,10q�5N       5O      �� 5P      $��1��6   .��5Q      �� $��Center=�5R   #   $	��5,0q�5S   #   ��5Q   #   �� *
MemoDtos+��5T      �� 5U      .R�5V   "   � 5W      ��5Y      �� 5Z      .R�5[   "   ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6F   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension     Kind$ ExpandIn=�.��5\   &   � $	��220��6+   $	��180��5]   +   $	��250��5^   +   $��5q�5_   +   *	  $Microsoft.Xaml.Behaviors.Interaction 	  Triggers 5`   *   +
  %Microsoft.Xaml.Behaviors.EventTrigger
  5a   .    
  	EventName$ MouseLeftButtonUp��6D   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5b   2   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter 6O   ��    Command 5c   O   �� *DataContext.SelectedCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ItemsControl5f   /    5h   *   �� 5i   .   $
��Auto �6<   �� 5j   .   .+�5l   *   '  !MaterialDesignThemes.Wpf.PopupBox  $��0��6B    K� ZIndex$ 1��5m   B   $��Right=�5n   B      PopupContent. 5o   .   �� $��删除��65   ��5p   5   �� ��5q   5   �� *DataContext.DeleteCommand+ �� $ FindAncestor=� ItemsControl5t   +   �� 5u   *   $��0��61   $��2��5v   1   
��� ��� �� CornerRadius$ 5��5x   1   �� 5y   *   $��0��64   $��16(�5{   4   $
��Bold"�5|   4   $
��10,5q�5}   4   ��5z   4   �� *Title+5}   4   �� 5~   *   $��1��64   $
��10,5q�5�   4   ��5   4   �� *	Content+5�   4   �� 5�   *   $��0��61   $��2��5�   1   $
}�True��5�   1   .+�5�   .   �� 
 �� Top$ 10��65    �� Right$	 -50��5�   5   $	��120��5�   5   $	��120��5�   5   $	 100��5�   5   ���#FFFFFF J� Opacity$	 0.1Q�5�   5   �� 5�   .   $ 80��65   $	 -30��5�   5   $	��120��5�   5   $	��120��5�   5   $	 100��5�   5   ���#FFFFFF$	 0.1Q�5�   5   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      