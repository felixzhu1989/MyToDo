using System.Windows;
using System.Windows.Input;

namespace MyToDo.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
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
    }
}