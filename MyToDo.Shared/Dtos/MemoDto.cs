namespace MyToDo.Shared.Dtos;
public class MemoDto:BaseDto
{
    private string title;
    public string Title
    {
        get => title;
        set { title = value; OnPropertyChanged(); }
    }
    private string content;
    public string Content
    {
        get => content;
        set { content = value; OnPropertyChanged(); }
    }
}