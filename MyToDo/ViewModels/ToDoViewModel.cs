using System.Collections.ObjectModel;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;

namespace MyToDo.ViewModels;

internal class ToDoViewModel:BindableBase
{
    public DelegateCommand AddCommand { get;}
    private bool isRightDrawerOpen;
    /// <summary>
    /// 右侧窗口是否展开
    /// </summary>
    public bool IsRightDrawerOpen
    {
        get => isRightDrawerOpen;
        set { isRightDrawerOpen = value; RaisePropertyChanged();}
    }

    private ObservableCollection<ToDoDto> toDoDtos;
    public ObservableCollection<ToDoDto> ToDoDtos
    {
        get => toDoDtos;
        set { toDoDtos = value; RaisePropertyChanged();}
    }

    public ToDoViewModel()
    {
        ToDoDtos=new ObservableCollection<ToDoDto>();
        CreateToDoList();
        AddCommand = new DelegateCommand(Add);
    }
    /// <summary>
    /// 添加待办
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    private void Add()
    {
        IsRightDrawerOpen=true;
    }

    private void CreateToDoList()
    {
        for (int i = 0; i < 10; i++)
        {
            ToDoDtos.Add(new ToDoDto{Title = $"待办标题{i}",Content = "待办事项..."});
        }
    }
}