using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Events;
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
    private string password;    
    public string Password
    {
        get { return password; }
        set { password = value; RaisePropertyChanged(); }
    }
    private int selectedIndex;
    /// <summary>
    /// 切换页面（登录/注册）
    /// </summary>
    public int SelectedIndex
    {
        get { return selectedIndex; }
        set { selectedIndex = value; RaisePropertyChanged(); }
    }
    private RegisterUserDto user;
    public  RegisterUserDto User
    {
        get { return user; }
        set { user = value; RaisePropertyChanged(); }
    }
    public DelegateCommand<string> ExecuteCommand { get; }   
    private readonly ILoginService _loginService;
    public readonly IEventAggregator _aggregator;//事件聚合器，消息提示
    public LoginViewModel(ILoginService loginService,IEventAggregator aggregator)
    {
        _aggregator=aggregator;
        _loginService =loginService;        
        ExecuteCommand=new DelegateCommand<string>(Execute);
        User=new RegisterUserDto();        
    }
    private void Execute(string obj)
    {
        switch (obj)
        {
            case "Login": Login(); break;
            case "Logout": Logout(); break;
            case "Go":SelectedIndex=1; break;//跳转注册页面
            case "Register": Register(); break;
            case "Return": SelectedIndex=0; break;//返回登录页面
        }
    }
    /// <summary>
    /// 注册账户
    /// </summary>    
    private async void Register()
    {
        if (string.IsNullOrWhiteSpace(User.Account)||
            string.IsNullOrWhiteSpace(User.UserName)|| 
            string.IsNullOrWhiteSpace(User.Password)||
            string.IsNullOrWhiteSpace(User.NewPassword))
            return;
        if (User.Password!=User.NewPassword)
        {
            //验证失败提示...
            _aggregator.SendMessage("两次输入的密码不一致，请检查！","Login");
            return; 
        }
        var registerResult= await _loginService.RegisterAsync(new UserDto
        {
            UserName=User.UserName,
            Account=User.Account,
            Password=User.Password            
        });
        if (registerResult!=null&&registerResult.Status)
        {
            //注册成功，切换到登录界面
            _aggregator.SendMessage("注册成功，请登录！", "Login");
            SelectedIndex=0;
            Account=User.Account;
            Password=User.Password;
            return;
        }
        //注册失败提示...
        _aggregator.SendMessage(registerResult.Message, "Login");
    }

    /// <summary>
    /// 登录操作
    /// </summary>
    private async void Login()
    {
        if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password))
            return;
        var loginResult = await _loginService.LoginAsync(new Shared.Dtos.UserDto
        {
            Account = Account,
            Password = Password
        });
        if (loginResult!=null&& loginResult.Status)
        {
            AppSession.UserName=loginResult.Result.UserName;
            _aggregator.SendMessage("登录成功，欢迎使用备忘录！");//默认即发到主页
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            return;
        }
        //登录失败提示...
        _aggregator.SendMessage(loginResult.Message, "Login");
    }
    /// <summary>
    /// 退出操作
    /// </summary>
    private void Logout()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.No));
    }
    public bool CanCloseDialog()
    {
        return true;
    }
    public void OnDialogClosed()
    {
        Logout();
    }
    public void OnDialogOpened(IDialogParameters parameters)
    {
    }
}
