using System.Collections.ObjectModel;
using MyToDo.Common.Models;
using Prism.Mvvm;
namespace MyToDo.ViewModels;
public class MainViewModel : BindableBase
{
    private ObservableCollection<MenuBar> menuBars;
    /// <summary>
    /// 菜单集合
    /// </summary>
    public ObservableCollection<MenuBar> MenuBars
    {
        get => menuBars;
        set { menuBars = value; RaisePropertyChanged(); }
    }
    public MainViewModel()
    {
        MenuBars = new();
        CreateMenuBar();
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
}
