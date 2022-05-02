using System.Windows;
using System.Windows.Input;
using MyToDo.Extensions;
using Prism.Events;

namespace MyToDo.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : Window
{
    public MainView(IEventAggregator aggregator)
    {
        InitializeComponent();
        //注册等待消息窗口
        aggregator.Register(arg =>
        {
            DialogHost.IsOpen = arg.IsOpen;
            if (DialogHost.IsOpen) DialogHost.DialogContent = new ProcessView();
        });
        ColorZone.MouseMove += (s, e) =>
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        };
        ColorZone.MouseDoubleClick += (s, e) =>
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        };
        BtnMin.Click += (s, e) => { WindowState = WindowState.Minimized; };
        BtnMax.Click += (s, e) =>
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        };
        BtnClose.Click += (s, e) => { Close(); };
        MenuBar.SelectionChanged += (s, e) =>
        {
            DrawerHost.IsLeftDrawerOpen = false;
        };
    }
}