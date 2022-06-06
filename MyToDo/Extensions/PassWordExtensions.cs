using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace MyToDo.Extensions;
public class PassWordExtensions
{
    //附加属性
    public static readonly DependencyProperty PassWordProperty = DependencyProperty.RegisterAttached("PassWord", typeof(string), typeof(PassWordExtensions), new PropertyMetadata(string.Empty,OnPassWordPropertyChanged));
    public static void OnPassWordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs  e)
    {
        var passWord = sender as PasswordBox;
        string passWordStr = (string)e.NewValue;
        if(passWord!=null&&passWord.Password!= passWordStr)
        {
            passWord.Password=passWordStr;
        }
    }    
    public static string GetPassWord(DependencyObject obj)
    {
        return (string)obj.GetValue(PassWordProperty);
    }
    public static void SetPassWord(DependencyObject obj, string value)
    {
        obj.SetValue(PassWordProperty,value);
    }
}
public class PassWordBehavior : Behavior<PasswordBox>
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
        PasswordBox passwordBox = sender as PasswordBox;
        string password = PassWordExtensions.GetPassWord(passwordBox!);
        if(password!=null && passwordBox!.Password!=password)
        {
            PassWordExtensions.SetPassWord(passwordBox,password);
        }
    }
}