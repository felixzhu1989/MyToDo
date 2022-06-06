using MyToDo.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
namespace MyToDo.ViewModels.Dialogs;
public class LoginViewModel : BindableBase, IDialogAware
{
    public string Title { get; set; } = "ToDo";
    public event Action<IDialogResult> RequestClose;
    private string account;
    public string Account
    {
        get { return account; }
        set { account = value; RaisePropertyChanged(); }
    }
    private string passWord;
    private readonly ILoginService _loginService;

    public string PassWord
    {
        get { return passWord; }
        set { passWord = value; RaisePropertyChanged(); }
    }
    public DelegateCommand<string> ExecuteCommand { get; }
    public LoginViewModel(ILoginService loginService)
    {
        ExecuteCommand=new DelegateCommand<string>(Execute);
        _loginService=loginService;
    }
    private void Execute(string obj)
    {
        switch (obj)
        {
            case "Login": Login(); break;
            case "LoginOut": LoginOut(); break;
            case "Register": Register(); break;
        }
    }    
    private async void Login()
    {
        if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(PassWord))
            return;
        var loginResult = await _loginService.LoginAsync(new Shared.Dtos.UserDto
        {
            Account = Account,
            Password = PassWord
        });
        if (loginResult.Status)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }
        //登录失败提示；

    }
    private void LoginOut()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.No));
    }
    public bool CanCloseDialog()
    {
        return true;
    }
    public void OnDialogClosed()
    {
        LoginOut();
    }
    public void OnDialogOpened(IDialogParameters parameters)
    {
    }
}
