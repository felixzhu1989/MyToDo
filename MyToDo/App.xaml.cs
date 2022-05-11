using System.Windows;
using DryIoc;
using MyToDo.Common;
using MyToDo.Service;
using MyToDo.ViewModels;
using MyToDo.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace MyToDo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
    }

    protected override void OnInitialized()
    {
        var service= App.Current.MainWindow!.DataContext as IConfigureService;
        service!.Configure();
        base.OnInitialized();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        //注册prism区域跳转导航页面
        containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>(); 
        containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>(); 
        containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>(); 
        containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
        containerRegistry.RegisterForNavigation<AboutView>();//没有ViewModel的情形

        //获取容器，然后注册HttpRestClient，并给构造函数设置默认值
        containerRegistry.GetContainer()
            .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "apiUrl"));
        containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5263/", serviceKey: "apiUrl");
        //注册服务
        containerRegistry.Register<IToDoService,ToDoService>();
        containerRegistry.Register<IMemoService,MemoService>();

    }
}