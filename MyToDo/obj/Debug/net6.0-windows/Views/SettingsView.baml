   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyToDo.ViewsMyToDo.Views  5      5clr-namespace:MyToDo.ExtensionsMyToDo.Extensions  5          MyToDo.Views.SettingsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:MyToDo.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.6.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      2
extensionsclr-namespace:MyToDo.Extensions   5      _	 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a;	behaviors+http://schemas.microsoft.com/xaml/behaviors 	 5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��设置��5      $��26(�5      $��15,10q�5      �� 5   
   $��0��6   ���#DDDDDD$��0,0,0,3q�5      6-   � 5   
   $��1��6   $
��0,10q�5       � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      .+�5!      �� -   5         ��Name$ MenuBar��5!         MyListBoxItemStyle#��[  5#      ��5"      �� *
MenuBars+* 	 $Microsoft.Xaml.Behaviors.Interaction   Triggers 5$      + 	 %Microsoft.Xaml.Behaviors.EventTrigger  5%         	EventName$ SelectionChanged��6-   * 	 $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5&      2 	 ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *NavigateCommand+   CommandParameter 5'   8   ��  �� ElementName$ MenuBar��	 �� Path$	 SelectedItem�5(      ��5*      �� 5+      .R�5,      �� 
 �� Orientation$
 
Horizontal=�6%   
������ .+�5.      '  !MaterialDesignThemes.Wpf.PackIcon  $
��15,0q�5/   6      Kind 5.   6   �� *Icon+5/   6   �� 50      $
��10,0q�51   (   ��50   (   �� *Title+51   (   52      53      55      �� 57      $��1��6   $   MyToDo.Extensions.PrismManager   SettingsViewRegionName!  Prism.Regions.RegionManager   
RegionName# Z 58      $
��10,0q�59      5:      5;      5<      