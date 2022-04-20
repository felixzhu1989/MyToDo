using System.Collections.ObjectModel;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;

namespace MyToDo.ViewModels;

public class MemoViewModel:BindableBase
{
    public DelegateCommand AddCommand { get; }
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

    public MemoViewModel()
    {
        MemoDtos=new ObservableCollection<MemoDto>();
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
            MemoDtos.Add(new MemoDto { Title = $"备忘标题{i}", Content = "备忘事项..." });
        }
    }

}