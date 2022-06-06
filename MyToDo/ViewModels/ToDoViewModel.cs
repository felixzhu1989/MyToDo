using System;
using System.Collections.ObjectModel;
using System.Linq;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;

namespace MyToDo.ViewModels;

public class ToDoViewModel : NavigationViewModel
{
    private readonly IDialogHostService _dialogHost;
    public DelegateCommand<string> ExecuteCommand { get; }//根据提供的不同参数执行不同的逻辑
    public DelegateCommand<ToDoDto> SelectedCommand { get; }
    public DelegateCommand<ToDoDto> DeleteCommand { get; }
    private bool isRightDrawerOpen;
    /// <summary>
    /// 右侧窗口是否展开
    /// </summary>
    public bool IsRightDrawerOpen
    {
        get => isRightDrawerOpen;
        set { isRightDrawerOpen = value; RaisePropertyChanged(); }
    }
    private ObservableCollection<ToDoDto> toDoDtos;
    public ObservableCollection<ToDoDto> ToDoDtos
    {
        get => toDoDtos;
        set { toDoDtos = value; RaisePropertyChanged(); }
    }
    private ToDoDto currentDto;
    /// <summary>
    /// 当前Todo对象，添加或编辑
    /// </summary>
    public ToDoDto CurrentDto
    {
        get => currentDto;
        set { currentDto = value; RaisePropertyChanged(); }
    }
    private string search;
    /// <summary>
    /// 搜索条件属性
    /// </summary>
    public string Search
    {
        get => search;
        set { search = value; RaisePropertyChanged(); }
    }
    private int selectedIndex;
    /// <summary>
    /// 选中状态，用于搜索筛选
    /// </summary>
    public int SelectedIndex
    {
        get => selectedIndex;
        set { selectedIndex = value; RaisePropertyChanged(); }
    }


    private readonly IToDoService _service;
    public ToDoViewModel(IToDoService service, IContainerProvider containerProvider) : base(containerProvider)
    {
        _service = service;
        _dialogHost=containerProvider.Resolve<IDialogHostService>();
        ToDoDtos=new ObservableCollection<ToDoDto>();
        ExecuteCommand = new DelegateCommand<string>(Execute);
        SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
        DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
    }

    /// <summary>
    /// 各种执行方法
    /// </summary>
    private void Execute(string obj)
    {
        switch (obj)
        {
            case "Add": Add(); break;
            case "Query": GetDataAsync(); break;
            case "Save": Save(); break;
        }
    }
    /// <summary>
    /// 添加待办
    /// </summary>
    private void Add()
    {
        CurrentDto = new ToDoDto();
        IsRightDrawerOpen=true;
    }

    /// <summary>
    /// 选中待办弹出修改界面
    /// </summary>
    /// <param name="obj"></param>
    private async void Selected(ToDoDto obj)
    {
        try
        {
            UpdateLoading(true);//等待进度条
            var toDoResult = await _service.GetFirstOrDefault(obj.Id);
            if (toDoResult.Status)
            {
                CurrentDto = toDoResult.Result;
                IsRightDrawerOpen = true;//展开右边的窗口
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            UpdateLoading(false);
        }
    }
    /// <summary>
    /// 保存待办
    /// </summary>
    private async void Save()
    {
        //标题和内容不能为空
        if (string.IsNullOrWhiteSpace(CurrentDto.Title)||string.IsNullOrWhiteSpace(CurrentDto.Content)) return;
        try
        {
            UpdateLoading(true);
            if (CurrentDto.Id>0)//编辑ToDo
            {
                var updateResult = await _service.UpdateAsync(CurrentDto);
                if (updateResult.Status)
                {
                    var todo = ToDoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                    if (todo != null)
                    {
                        //更新界面显示
                        todo.Title = CurrentDto.Title;
                        todo.Content = CurrentDto.Content;
                        todo.Status = CurrentDto.Status;
                    }
                }
            }
            else//新增ToDo
            {
                var addResult = await _service.AddAsync(CurrentDto);
                if (addResult.Status)
                {
                    //更新界面显示
                    ToDoDtos.Add(addResult.Result!);
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            IsRightDrawerOpen = false;
            UpdateLoading(false);
        }
    }

    /// <summary>
    /// 删除待办
    /// </summary>
    /// <param name="obj"></param>
    private async void Delete(ToDoDto obj)
    {
        try
        {
            var dialogResult = await _dialogHost.Question("温馨提示", $"确认删除待办事项：{obj.Title}?");
            if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
            UpdateLoading(true);
            var deleteResult = await _service.DeleteAsync(obj.Id);
            if (deleteResult.Status)
            {
                var model = ToDoDtos.FirstOrDefault(T => T.Id.Equals(obj.Id));
                if (model != null) ToDoDtos.Remove(model);
            }
        }
        finally 
        {            
            UpdateLoading(false); 
        }
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    private async void GetDataAsync()
    {
        //模拟待办数据
        //之前的方法叫CreateToDoList();在构造函数中调用初始化数据
        //for (int i = 0; i < 10; i++)
        //{
        //    ToDoDtos.Add(new ToDoDto{Title = $"待办标题{i}",Content = "待办事项..."});
        //}
        UpdateLoading(true);//打开等待窗口
        int? status = SelectedIndex == 0 ? null : SelectedIndex == 1 ? 0 : 1;
        var toDoResult = await _service.GetAllFilterAsync(new ToDoParameter
        {
            PageIndex = 0,
            PageSize = 100,
            Search = Search,
            Status = status
        });
        if (toDoResult.Status)
        {
            ToDoDtos.Clear();
            foreach (var item in toDoResult.Result.Items)
            {
                ToDoDtos.Add(item);
            }
        }
        UpdateLoading(false);//数据加载完毕后关闭等待窗口
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        if (navigationContext.Parameters.ContainsKey("Value"))
            SelectedIndex= navigationContext.Parameters.GetValue<int>("Value");
        else
            SelectedIndex=0;
        GetDataAsync();
    }
}