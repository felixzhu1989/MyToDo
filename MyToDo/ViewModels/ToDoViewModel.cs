using System.Collections.ObjectModel;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using Prism.Commands;
using Prism.Mvvm;

namespace MyToDo.ViewModels;

public class ToDoViewModel:BindableBase
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

    private readonly IToDoService _service;
    public ToDoViewModel(IToDoService service)
    {
        _service = service;
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

    private async void CreateToDoList()
    {
        //模拟待办数据
        //for (int i = 0; i < 10; i++)
        //{
        //    ToDoDtos.Add(new ToDoDto{Title = $"待办标题{i}",Content = "待办事项..."});
        //}
       var toDoResult=await _service.GetAllAsync(new QueryParameter
        {
            PageIndex = 0,
            PageSize = 100
        });
       if (toDoResult.Status)
       {
           foreach (var item in toDoResult.Result.Items)
           {
               ToDoDtos.Add(item);
           }
       }
    }
}