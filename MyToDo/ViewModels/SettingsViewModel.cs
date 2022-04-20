using System.Collections.ObjectModel;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels;

public class SettingsViewModel:BindableBase
{
    private readonly IRegionManager _regionManager;
    private ObservableCollection<MenuBar> menuBars;
    public ObservableCollection<MenuBar> MenuBars
    {
        get => menuBars;
        set { menuBars = value; RaisePropertyChanged(); }
    }
    public DelegateCommand<MenuBar> NavigateCommand { get; }

    public SettingsViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        MenuBars = new ObservableCollection<MenuBar>();
        CreateMenuBar();
        NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
    }
    private void Navigate(MenuBar? obj)
    {
        if (obj==null||string.IsNullOrWhiteSpace(obj.NameSpace)) return;
        _regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
    }

    /// <summary>
    /// 初始化菜单集合
    /// </summary>
    void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
        MenuBars.Add(new MenuBar { Icon = "Cog", Title = "系统设置", NameSpace = "" });
        MenuBars.Add(new MenuBar { Icon = "Information", Title = "关于更多", NameSpace = "AboutView" });
    }

}