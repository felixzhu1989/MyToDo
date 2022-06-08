using System.Collections.ObjectModel;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels;
public class MainViewModel : BindableBase,IConfigureService
{
    private readonly IRegionManager _regionManager;
    private readonly IContainerProvider _container;
    private IRegionNavigationJournal _journal;
    private ObservableCollection<MenuBar> menuBars;
    public ObservableCollection<MenuBar> MenuBars
    {
        get => menuBars;
        set { menuBars = value; RaisePropertyChanged(); }
    }
    private string userName;
    public string UserName
    {
        get { return userName; }
        set { userName = value;RaisePropertyChanged(); }
    }
    public DelegateCommand<MenuBar> NavigateCommand { get;}
    public DelegateCommand GoBackCommand { get;}
    public DelegateCommand GoForwardCommand { get;}
    public DelegateCommand LogoutCommand { get; }
    public MainViewModel(IRegionManager regionManager, IContainerProvider container)
    {
        _regionManager = regionManager;
        _container=container;        
        MenuBars = new ObservableCollection<MenuBar>();
        NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
        GoBackCommand = new DelegateCommand(() =>
        {
            if (_journal is { CanGoBack: true }) _journal.GoBack();
        });
        GoForwardCommand = new DelegateCommand(() =>
        {
            if (_journal is { CanGoForward: true }) _journal.GoForward();
        });
        LogoutCommand=new DelegateCommand(() => { App.Logout(_container); });//注销登录
    }

    private void Navigate(MenuBar? obj)
    {
        if(obj==null||string.IsNullOrWhiteSpace(obj.NameSpace))return;
        _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
        {
            _journal = back.Context.NavigationService.Journal;
        });
    }

    /// <summary>
    /// 初始化菜单集合
    /// </summary>
    void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
        MenuBars.Add(new MenuBar { Icon = "Notebook", Title = "待办事项", NameSpace = "ToDoView" });
        MenuBars.Add(new MenuBar { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
        MenuBars.Add(new MenuBar { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
    }
    /// <summary>
    /// 初始化配置默认首页
    /// </summary>
    public void Configure()
    {
        CreateMenuBar();
        UserName=AppSession.UserName;        
        _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
    }
}
