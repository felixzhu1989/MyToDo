   M S B A M L   `   `   `  ����A  =MyToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MyToDoMyToDo  5   	   5clr-namespace:MyToDo.ExtensionsMyToDo.Extensions  5   	       MyToDo.Views.MainView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:MyToDo     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   2
extensionsclr-namespace:MyToDo.Extensions   5   	   _	 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a:behavior+http://schemas.microsoft.com/xaml/behaviors 	 5   	     � Title$  
MainWindow��5   	      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	   $��微软雅黑)�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � AllowsTransparency$
 True��5   	   $	��600��5   	   $
��1024��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   !  Prism.Mvvm.ViewModelLocator   AutoWireViewModel True5   	   .��5      )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ 
DialogHost��5       	   
Identifier$	 
RootDialog��5       
   DialogTheme$
 Inherit=�5       .��5    
   )  #MaterialDesignThemes.Wpf.DrawerHost  -   5          Name$ 
DrawerHost��5    $      IsLeftDrawerOpen 5!   $   ��  �� ElementName$ MenuToggleButton�� �� Path$ 	IsChecked�   LeftDrawerContent 5"      ]� 5#      $	��220��6   .+�5$      �� $	��Top=�6!   $
��0,20q�5%   !   .+�5&      �� $��50��6    $��50��5'       $��../Images/user.jpg��5(        J� Clip 5)      @� 5*   "    @� Center$ 25,25&�62    @� RadiusX$ 25Q�5+   2    @� RadiusY$ 25Q�5,   2   5.      �� 5/      $��Center=�50   $   $
��0,10q�51   $   ��5/   $   �� *
UserName+51   $   52      �� 53      -   5         ��Name$ MenuBar��53        MyListBoxItemStyle#��[ 55      ��54      �� *
MenuBars+* 	 $Microsoft.Xaml.Behaviors.Interaction   Triggers 56      + 	 %Microsoft.Xaml.Behaviors.EventTrigger  57         	EventName$ SelectionChanged��64   * 	 $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 58   "   2 	 ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6?   �� *NavigateCommand+   CommandParameter 59   ?   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" ��$ SelectedItem�5:      ��5<      �� 5=      .R�5>   "   ��  �� Orientation$ 
Horizontal=�6-   
������ .+�5@   &   '	  !MaterialDesignThemes.Wpf.PackIcon	  $
��15,0q�5A   >    	  Kind 5@   >   �� *Icon+5A   >   �� 5B   &   $
��10,0q�5C   0   ��5B   0   �� *Title+5C   0   5D   #   5E      5G      5H      .��5K      �  � RowDefinitions 5L      �� 5M      $
��auto �6$   �� 5N      .+�5P      '
  !MaterialDesignThemes.Wpf.Snackbar
  -   5          
 Name$  Snackbar��5P   *   ! K� ZIndex$! 1��5R   *   " 
  MessageQueue" 5Q   *   4  .MaterialDesignThemes.Wpf.MessageQueueExtension  5R   *   (  "MaterialDesignThemes.Wpf.ColorZone  5S      -   5        #  Name$# 	ColorZone��5S   +   $��16q�5T   +   +  %MaterialDesignThemes.Wpf.ShadowAssist$   ShadowDepth$$ Depth2=�5U   +   %   Mode$% 
PrimaryMid=�5V   +   $	��Top=�5W   +   .��5X      ]� $
��True��6    .+�5Y      �� $ 
Horizontal=�6%   $��Right=�5Z   %   .+�5[      �� -   5        & ��Name$& BtnMin��5[   %   $	��—��5\   %    ! MaterialDesignFlatMidBgButton#��[ 5]   %   �� 5^      -   5        $& BtnMax��5^   %   $	��☐��5_   %   #��[ 5`   %   �� 5a      -   5        $& BtnClose��5a   %   $	��✕��5b   %   #��[ 5c   %   5d      '  !MaterialDesignThemes.Wpf.PopupBox  5e      $��Right=�62   '   ToggleContent' 5f      �� 5g   "   $��25��6(   $��25��5h   (   $
��16,0q�5i   (   $��../Images/user.jpg��5j   (    5k   &   @� 5l   *   $ 	12.5,12.5&�6:   $
 12.5Q�5m   :   $
 12.5Q�5n   :   5p   #   (   PopupContent.( 5r      �� .+�5s   "   �� $��个人中心��6)   ��5t   )   �� *AppCenterCommand+�� 5u   "   $��注销登录��6)   ��5v   )   �� *LogoutCommand+5w      5x      �� 5y      $ 
Horizontal=�6%   .+�5z      d� -	   5        ) d�Name$) MenuToggleButton��5z   +    ' #MaterialDesignHamburgerToggleButton#��[ 5{   +   * d� 	IsChecked$* FalseY�5|   +   4  .System.Windows.Automation.AutomationProperties+   Name$+ HamburgerToggleButton��5}   +   �� 5~      $��24,0,0,0q�6%   , � ToolTip$, Previous Item��5   %     MaterialDesignToolButton#��[ 5�   %   ��5�   %   �� *GoBackCommand+��5�   %   0  *MaterialDesignThemes.Wpf.PackIconExtension  -   Kind$- 	ArrowLeft=�.   Size. 24��5�   %   ��  �� " �$ (TextElement.Foreground)�+  %MaterialDesignThemes.Wpf.RippleAssist/   Feedback/ 5�   %   ��  �� *Self+$ 
Foreground�  BrushRoundConverter0 �� 	Converter#0 [ �� 5�      $��16,0,0,0q�6%   $, 	Next Item��5�   %   #��[ 5�   %   ��5�   %   �� *GoForwardCommand+��5�   %     $- 
ArrowRight=�. 24��5�   %   ��  �� " �$ (TextElement.Foreground)�/ 5�   %   ��  �� *Self+$ 
Foreground�#0 [ �� 5�      $��Center=�6(   $��Center=�5�   (   $��22(�5�   (   $
��16,0q�5�   (   $+ MyToDo��5�   (   $��MyToDo��5�   (   5�      5�      5�      �� 5�      $��1��6!   $   MyToDo.Extensions.PrismManager1   MainViewRegionName!  Prism.Regions.RegionManager2   
RegionName#2 Z1 6.   5�      5�      5�      5�      