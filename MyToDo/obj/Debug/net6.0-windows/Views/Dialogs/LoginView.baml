   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MyToDo.Views.DialogsMyToDo.Views.Dialogs  5      5clr-namespace:MyToDo.ExtensionsMyToDo.Extensions  5
      $    MyToDo.Views.Dialogs.LoginView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:MyToDo.Views.Dialogs     5      _ [MaterialDesignThemes.Wpf, Version=4.6.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5	      .pwdExtclr-namespace:MyToDo.Extensions   5
      _	 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a:behavior+http://schemas.microsoft.com/xaml/behaviors 	 5      $	��600��5      $	��350��5      #  Prism.Services.Dialogs.Dialog    WindowStyle  5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 600��5      �� 5       � ��$	 350��5      �� 5       �@�SizeToContent$ WidthAndHeight=�5      �� 5       �@�
ResizeMode$ NoResize=�5      �� 5       �@ WindowStartupLocation$ CenterScreen=�5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      .+�5!   
   �� $��../../Images/login.png��6   $��50q�5"      '  !MaterialDesignThemes.Wpf.Snackbar  5#   
   -   5          Name$ LoginSnackBar��5#   "    K� ZIndex$ 1��5%   "   $��2��5&   "      MessageQueue 5$   "   4  .MaterialDesignThemes.Wpf.MessageQueueExtension  5&   "   7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5'   
   $��1��6&    �� SelectedIndex 5(   &   �� *SelectedIndex+.@�5)      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .��5*      ]� $��Center=�6   $��15q�5+      .+�5,      �� $��欢迎使用��6    $��22(�5-       $
��Bold"�5.       $
��0,10q�5/       $	��Top=�50       �� 51      )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 请输入账号��6   $
��0,10q�53      $	��Top=�54      ��52      �� *	Account+54      G� 55      $	 请输入密码��6"   $
��0,10q�57   "   $	��Top=�58   "   *   $MyToDo.Extensions.PasswordExtensions
   Password
 56   "   �� *
Password+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�* 	 $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 59      (	   "MyToDo.Extensions.PasswordBehavior	  5:      5<      �� 5=      $��登录系统��6   $��Login��5?      $
��0,10q�5@      $	��Top=�5A      ��5>      �� *ExecuteCommand+5A      ]� 5B      $��False��6    .+�5C      �� $��注册账号��6$      Triggers 5D      +
 	 %Microsoft.Xaml.Behaviors.EventTrigger
  5E   "    
  	EventName$ MouseLeftButtonDown��68   * 	 $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5F   &   2 	 ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ Go��5G   C      Command 5F   C   �� *ExecuteCommand+5G   C   5H   #   5J      �� 5K      $��忘记密码��6$   $��Right=�5L   $   5M      5N      5O        5P      .��5Q      ]� $��Center=�6   $��15q�5R      .+�5S      �� $��注册账号��6    $��22(�5T       $
��Bold"�5U       $
��0,10q�5V       $	��Top=�5W       �� 5X      $	 请输入账号��6   $
��0,10q�5Z      $	��Top=�5[      ��5Y      �� *User.Account+5[      �� 5\      $	 请输用户名��6   $
��0,10q�5^      $	��Top=�5_      ��5]      �� *User.UserName+5_      G� 5`      $	 请输入密码��6"   $
��0,10q�5b   "   $	��Top=�5c   "   
 5a   "   �� *User.Password+$ TwoWay=�$ PropertyChanged=� 5d      	  5e      5g      G� 5h      $	 请再次输入密码��6"   $
��0,10q�5j   "   $	��Top=�5k   "   
 5i   "   �� *User.NewPassword+$ TwoWay=�$ PropertyChanged=� 5l      	  5m      5o      �� 5p      $��注册账户��6   $��Register��5r      $	��0,5q�5s      $	��Top=�5t      ��5q      �� *ExecuteCommand+5t      �� 5u      $��返回登录��6   $��Return��5w      $	��0,5q�5x      $	��Top=�5y      ��5v      �� *ExecuteCommand+5y      5z      5{      5|      5}      5~      