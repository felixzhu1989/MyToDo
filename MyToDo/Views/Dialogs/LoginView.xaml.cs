using MyToDo.Extensions;
using Prism.Events;
using System.Windows.Controls;

namespace MyToDo.Views.Dialogs;

/// <summary>
/// LoginView.xaml 的交互逻辑
/// </summary>
public partial class LoginView : UserControl
{    
    public LoginView(IEventAggregator aggregator)
    {
        InitializeComponent();
        //注册消息提示
        aggregator.RegisterMessage(arg =>
        {
            LoginSnackBar.MessageQueue.Enqueue(arg.Message);
        },"Login");
    }
}
