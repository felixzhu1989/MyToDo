using System.Collections.ObjectModel;
namespace MyToDo.Shared.Dtos;
/// <summary>
/// 汇总数据
/// </summary>
public class SummaryDto:BaseDto
{
    private int sum;
    /// <summary>
    /// 单板事项总数
    /// </summary>
    public int Sum
    {
        get { return sum; }
        set { sum = value; OnPropertyChanged(); }
    }
    private int completedCount;
    /// <summary>
    /// 待办事项完成数量
    /// </summary>
    public int CompletedCount
    {
        get { return completedCount; }
        set { completedCount = value; OnPropertyChanged(); }
    }
    private int memoCount;
    /// <summary>
    /// 备忘录数量
    /// </summary>
    public int MemoCount
    {
        get { return memoCount; }
        set { memoCount = value; OnPropertyChanged(); }
    }
    private string completedRatio;
    /// <summary>
    /// 完成比例
    /// </summary>
    public string CompletedRatio
    {
        get { return completedRatio; }
        set { completedRatio = value; OnPropertyChanged(); }
    }
    private ObservableCollection<ToDoDto>   toDoDtos;
    /// <summary>
    /// 待办事项列表
    /// </summary>
    public ObservableCollection<ToDoDto> ToDoDtos
    {
        get { return toDoDtos; }
        set { toDoDtos = value; OnPropertyChanged(); }
    }
    private ObservableCollection<MemoDto> memoDtos;
    /// <summary>
    /// 备忘录列表
    /// </summary>
    public ObservableCollection<MemoDto> MemoDtos
    {
        get { return memoDtos; }
        set { memoDtos = value; OnPropertyChanged(); }
    }
}
