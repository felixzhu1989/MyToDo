namespace MyToDo.Shared.Dtos;

public class UserDto:BaseDto
{
    private string account;

    public string Account
    {
        get => account;
        set { account = value; OnPropertyChanged();}
    }
    private string userName;

    public string UserName
    {
        get => userName;
        set { userName = value; OnPropertyChanged(); }
    }
    private string password;

    public string Password
    {
        get => password;
        set { password = value; OnPropertyChanged(); }
    }
}