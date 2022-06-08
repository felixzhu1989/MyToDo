using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
namespace MyToDo.Extensions;
/// <summary>
/// 行为，让PasswordBox支持绑定
/// </summary>
public class PasswordBehavior : Behavior<PasswordBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PasswordChanged+=AssociatedObject_PasswordChanged;
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PasswordChanged-=AssociatedObject_PasswordChanged;
    }
    private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordBox passwordBox = (PasswordBox)sender;
        string passwordStr = PasswordExtensions.GetPassword(passwordBox);
        if (passwordBox!=null && passwordBox.Password!=passwordStr)
        {
            //将密码输入框中的最新值赋值给附加属性
            PasswordExtensions.SetPassword(passwordBox, passwordBox.Password);
        }
    }
}