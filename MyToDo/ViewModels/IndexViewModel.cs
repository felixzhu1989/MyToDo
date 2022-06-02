using System;
using System.Collections.ObjectModel;
using System.Linq;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;
namespace MyToDo.ViewModels;
public class IndexViewModel : NavigationViewModel
{
    private ObservableCollection<TaskBar> taskBars;
    public ObservableCollection<TaskBar> TaskBars
    {
        get => taskBars;
        set { taskBars = value; RaisePropertyChanged(); }
    }
    //private ObservableCollection<ToDoDto> toDoDtos;
    //public ObservableCollection<ToDoDto> ToDoDtos
    //{
    //    get => toDoDtos;
    //    set { toDoDtos = value; RaisePropertyChanged(); }
    //}
    //private ObservableCollection<MemoDto> memoDtos;
    //public ObservableCollection<MemoDto> MemoDtos
    //{
    //    get => memoDtos;
    //    set { memoDtos = value; RaisePropertyChanged(); }
    //}
    private SummaryDto summary;
    public SummaryDto Summary
    {
        get { return summary; }
        set { summary = value; RaisePropertyChanged(); }
    }
    private string title;
    public string Title
    {
        get { return title; }
        set { title = value; RaisePropertyChanged(); }
    }

    public DelegateCommand<string> ExecuteCommand { get; }
    public DelegateCommand<ToDoDto> EditToDoCommand { get; }
    public DelegateCommand<MemoDto> EditMemoCommand { get; }
    public DelegateCommand<ToDoDto> ToDoCompletedCommand { get; }
    public DelegateCommand<TaskBar> NavigateCommand { get; }

    private readonly IDialogHostService _dialog;
    private readonly IToDoService _toDoService;
    private readonly IMemoService _memoService;
    private readonly IRegionManager _regionManager;
    
    public IndexViewModel(IContainerProvider provider, IDialogHostService dialog) : base(provider)
    {
        _dialog=dialog;
        _toDoService=provider.Resolve<IToDoService>();
        _memoService=provider.Resolve<IMemoService>();
        _regionManager=provider.Resolve<IRegionManager>();
        TaskBars = new ObservableCollection<TaskBar>();
        //ToDoDtos = new ObservableCollection<ToDoDto>();
        //MemoDtos = new ObservableCollection<MemoDto>();
        CreateTaskBars();
        //CreateTestData();
        ExecuteCommand=new DelegateCommand<string>(Execute);
        EditToDoCommand=new DelegateCommand<ToDoDto>(AddToDo);
        EditMemoCommand=new DelegateCommand<MemoDto>(AddMemo);
        ToDoCompletedCommand=new DelegateCommand<ToDoDto>(Completed);
        NavigateCommand=new DelegateCommand<TaskBar>(Navigate);
        Title=$"您好，宏峰!今天是{DateTime.Now.GetDateTimeFormats('D')[1]}。";
    }

    private void Navigate(TaskBar obj)
    {
        if (string.IsNullOrEmpty(obj.Target)) return;
        NavigationParameters param=new NavigationParameters();
        if (obj.Title=="已完成")
        {
            param.Add("Value", 2);
        }
        _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.Target,param);
    }

    private async void Completed(ToDoDto obj)
    {
        var updateResult = await _toDoService.UpdateAsync(obj);//修改数据
        if (updateResult.Status)
        {
           var todo= Summary.ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
            if (todo!=null)
            {
                Summary.ToDoDtos.Remove(todo);//从界面列表删除
                Summary.CompletedCount++;
                Summary.CompletedRatio=(Summary.CompletedCount/(double)Summary.Sum).ToString("0%");
                Refresh();
            }
        }
    }

    private void Execute(string obj)
    {
        switch (obj)
        {
            case "AddToDo":
                AddToDo(null);
                break;
            case "AddMemo":
                AddMemo(null);
                break;
        }
    }
    /// <summary>
    /// 添加代办事项
    /// </summary>
    async void AddToDo(ToDoDto? model)
    {
        DialogParameters param = new DialogParameters();
        if (model!=null) param.Add("Value", model);

        var dialogResult = await _dialog.ShowDialog("AddToDoView", param);
        if (dialogResult.Result==ButtonResult.OK)
        {
            var toDo = dialogResult.Parameters.GetValue<ToDoDto>("Value");
            if (toDo.Id>0)
            {
                //修改
                var updateResult = await _toDoService.UpdateAsync(toDo);
                if (updateResult.Status)
                {
                    var toDoModel = Summary.ToDoDtos.FirstOrDefault(t => t.Id.Equals(toDo.Id));
                    if (toDoModel!=null)
                    {
                        toDoModel.Title=toDo.Title;
                        toDoModel.Content=toDo.Content;
                    }
                }
            }
            else
            {
                //新增
                var addResult = await _toDoService.AddAsync(toDo);
                if (addResult.Status)
                {
                    Summary.ToDoDtos.Add(addResult.Result!);//界面显示
                    Summary.Sum++;
                    Summary.CompletedRatio=(Summary.CompletedCount/(double)Summary.Sum).ToString("0%");
                    Refresh();
                }
            }
        }
    }
    /// <summary>
    /// 添加备忘录
    /// </summary>
    async void AddMemo(MemoDto? model)
    {
        DialogParameters param = new DialogParameters();
        if (model!=null) param.Add("Value", model);
        var dialogResult = await _dialog.ShowDialog("AddMemoView", param);
        if (dialogResult.Result==ButtonResult.OK)
        {
            var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
            if (memo.Id>0)
            {
                //修改
                var updateResult = await _memoService.UpdateAsync(memo);
                if (updateResult.Status)
                {
                    var memoModel = Summary.MemoDtos.FirstOrDefault(t => t.Id.Equals(memo.Id));
                    if (memoModel!=null)
                    {
                        memoModel.Title=memo.Title;
                        memoModel.Content=memo.Content;
                    }
                }
            }
            else
            {
                //新增
                var addResult = await _memoService.AddAsync(memo);
                if (addResult.Status)
                {
                    Summary.MemoDtos.Add(addResult.Result!);
                    Summary.MemoCount++;
                    Refresh();
                }
            }
        }
    }

    private void CreateTestData()
    {
        //ToDoDtos = new ObservableCollection<ToDoDto>();
        //MemoDtos = new ObservableCollection<MemoDto>();
        for (int i = 0; i < 5; i++)
        {
            Summary.ToDoDtos.Add(new ToDoDto { Title = $"待办{i}", Content = "正在处理中..." });
            Summary.MemoDtos.Add(new MemoDto { Title = $"备忘{i}", Content = "备忘事项..." });
        }
    }


    void CreateTaskBars()
    {
        TaskBars.Add(new TaskBar { Icon = "ClockFast", Title = "汇总",  Color = "DodgerBlue", Target = "ToDoView" });
        TaskBars.Add(new TaskBar { Icon = "AlarmCheck", Title = "已完成",  Color = "LimeGreen", Target = "ToDoView" });
        TaskBars.Add(new TaskBar { Icon = "ChartLine", Title = "完成率",  Color = "SkyBlue", Target = "" });
        TaskBars.Add(new TaskBar { Icon = "PlaylistStar", Title = "备忘录",  Color = "Orange", Target = "MemoView" });
    }
    public override async void OnNavigatedTo(NavigationContext navigationContext)
    {
        
        var summaryResult = await _toDoService.SummaryAsync();
        if (summaryResult.Status)
        {
            Summary=summaryResult.Result!;
            Refresh();
        }
        base.OnNavigatedTo(navigationContext);
    }
    void Refresh()
    {        
        TaskBars[0].Content=Summary.Sum.ToString();
        TaskBars[1].Content=Summary.CompletedCount.ToString();
        TaskBars[2].Content=Summary.CompletedRatio;
        TaskBars[3].Content=Summary.MemoCount.ToString();
    }
}