using System.Windows;
using System.Windows.Input;
using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Events;

namespace MyToDo.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : Window
{
    private readonly IDialogHostService _dialogHost;
    public MainView(IEventAggregator aggregator, IDialogHostService dialogHost)
    {
        _dialogHost=dialogHost;
        InitializeComponent();
        //注册snackbar提示消息
        aggregator.RegisterMessage(arg =>
        {
            Snackbar.MessageQueue!.Enqueue(arg);//往消息队列中添加消息
        });

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
        BtnClose.Click += async(s, e) =>
        {
            var dialogResult =await _dialogHost.Question("温馨提示", $"确认退出系统吗?");
            if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
            Close();
        };
        MenuBar.SelectionChanged += (s, e) =>
        {
            DrawerHost.IsLeftDrawerOpen = false;
        };

    }
}