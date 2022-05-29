using System.Collections.ObjectModel;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;

namespace MyToDo.ViewModels;

public class IndexViewModel:BindableBase
{
    private ObservableCollection<TaskBar> taskBars;
    public ObservableCollection<TaskBar> TaskBars
    {
        get => taskBars;
        set { taskBars = value; RaisePropertyChanged();}
    }
    private ObservableCollection<ToDoDto> toDoDtos;
    public ObservableCollection<ToDoDto> ToDoDtos
    {
        get => toDoDtos;
        set { toDoDtos = value; RaisePropertyChanged(); }
    }
    private ObservableCollection<MemoDto> memoDtos;
    private readonly IDialogHostService _dialog;

    public ObservableCollection<MemoDto> MemoDtos
    {
        get => memoDtos;
        set { memoDtos = value; RaisePropertyChanged(); }
    }
    public DelegateCommand<string> ExecuteCommand { get; } 

    public IndexViewModel(IDialogHostService dialog)
    {        
        TaskBars = new ObservableCollection<TaskBar>();
        ToDoDtos = new ObservableCollection<ToDoDto>();
        MemoDtos = new ObservableCollection<MemoDto>();
        CreateTaskBars();
        //CreateTestData();
        ExecuteCommand=new DelegateCommand<string>(Execute);
        _dialog=dialog;
    }

    private void Execute(string obj)
    {
        switch (obj)
        {
            case "AddToDo":
                AddToDo();
                break;
            case "AddMemo":
                AddMemo();
                break;
        }
    }
    void AddToDo()
    {
        _dialog.ShowDialog("AddToDoView",null);
    }
    void AddMemo()
    {
        _dialog.ShowDialog("AddMemoView", null);
    }

    private void CreateTestData()
    {
        //ToDoDtos = new ObservableCollection<ToDoDto>();
        //MemoDtos = new ObservableCollection<MemoDto>();
        for (int i = 0; i < 5; i++)
        {
            ToDoDtos.Add(new ToDoDto{Title = $"待办{i}",Content = "正在处理中..."});
            MemoDtos.Add(new MemoDto{ Title = $"备忘{i}", Content = "备忘事项..." });
        }
    }


    void CreateTaskBars()
    {
        TaskBars.Add(new TaskBar{Icon = "ClockFast", Title = "汇总",Content = "9",Color = "#FF0CA0FF",Target = ""});
        TaskBars.Add(new TaskBar{Icon = "AlarmCheck", Title = "已完成",Content = "9",Color = "#FF1ECA3A",Target = ""});
        TaskBars.Add(new TaskBar{Icon = "ChartLine", Title = "完成率",Content = "100%",Color = "#FF02C6DC",Target = ""});
        TaskBars.Add(new TaskBar{Icon = "PlaylistStar", Title = "备忘录",Content = "19",Color = "#FFFFA000",Target = ""});
    }
   
}