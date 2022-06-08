namespace MyToDo.Shared.Dtos;
public class RegisterUserDto: UserDto
{    
    private string newPassword;
    public string NewPassword
    {
        get => newPassword;
        set { newPassword = value; OnPropertyChanged(); }
    }
}
