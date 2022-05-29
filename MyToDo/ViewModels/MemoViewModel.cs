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
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels;

public class MemoViewModel : NavigationViewModel
{
    private readonly IDialogHostService _dialogHost;
    public DelegateCommand<string> ExecuteCommand { get; }//根据提供的不同参数执行不同的逻辑
    public DelegateCommand<MemoDto> SelectedCommand { get; }
    public DelegateCommand<MemoDto> DeleteCommand { get; }
    private bool isRightDrawerOpen;
    /// <summary>
    /// 右侧窗口是否展开
    /// </summary>
    public bool IsRightDrawerOpen
    {
        get => isRightDrawerOpen;
        set { isRightDrawerOpen = value; RaisePropertyChanged(); }
    }
    private ObservableCollection<MemoDto> memoDtos;
    public ObservableCollection<MemoDto> MemoDtos
    {
        get => memoDtos;
        set { memoDtos = value; RaisePropertyChanged(); }
    }
    private MemoDto currentDto;
    /// <summary>
    /// 当前Todo对象，添加或编辑
    /// </summary>
    public MemoDto CurrentDto
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

    private readonly IMemoService _service;
    public MemoViewModel(IMemoService service, IContainerProvider containerProvider) : base(containerProvider)
    {
        _service = service;
        _dialogHost=containerProvider.Resolve<IDialogHostService>();
        MemoDtos=new ObservableCollection<MemoDto>();
        ExecuteCommand = new DelegateCommand<string>(Execute);
        SelectedCommand = new DelegateCommand<MemoDto>(Selected);
        DeleteCommand = new DelegateCommand<MemoDto>(Delete);
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
        CurrentDto = new MemoDto();
        IsRightDrawerOpen=true;
    }

    /// <summary>
    /// 选中待办弹出修改界面
    /// </summary>
    /// <param name="obj"></param>
    private async void Selected(MemoDto obj)
    {
        try
        {
            UpdateLoading(true);//等待进度条
            var memoResult = await _service.GetFirstOrDefault(obj.Id);
            if (memoResult.Status)
            {
                CurrentDto = memoResult.Result;
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
            if (CurrentDto.Id>0)//编辑Status
            {
                var updateResult = await _service.UpdateAsync(CurrentDto);
                if (updateResult.Status)
                {
                    var memo = MemoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                    if (memo != null)
                    {
                        //更新界面显示
                        memo.Title = CurrentDto.Title;
                        memo.Content = CurrentDto.Content;
                    }
                }
            }
            else//新增ToDo
            {
                var addResult = await _service.AddAsync(CurrentDto);
                if (addResult.Status)
                {
                    //更新界面显示
                    MemoDtos.Add(addResult.Result);
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
    private async void Delete(MemoDto obj)
    {
        try
        {
            var dialogResult = await _dialogHost.Question("温馨提示", $"确认删除待办事项：{obj.Title}?");
            if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
            UpdateLoading(true);
            var deleteResult = await _service.DeleteAsync(obj.Id);
            if (deleteResult.Status)
            {
                var model = MemoDtos.FirstOrDefault(T => T.Id.Equals(obj.Id));
                if (model != null) MemoDtos.Remove(model);
            }
        }

        finally
        {
            UpdateLoading(false);
        }
    }



    private async void GetDataAsync()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    MemoDtos.Add(new MemoDto { Title = $"备忘标题{i}", Content = "备忘事项..." });
        //}
        UpdateLoading(true);//打开等待窗口
        var memoResult = await _service.GetAllAsync(new QueryParameter
        {
            PageIndex = 0,
            PageSize = 100,
            Search = Search
        });
        if (memoResult.Status)
        {
            MemoDtos.Clear();
            foreach (var item in memoResult.Result!.Items)
            {
                MemoDtos.Add(item);
            }
        }
        UpdateLoading(false);//数据加载完毕后关闭等待窗口
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        GetDataAsync();
    }

}