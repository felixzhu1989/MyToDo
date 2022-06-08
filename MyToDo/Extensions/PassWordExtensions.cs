
using System.Windows;
using System.Windows.Controls;
namespace MyToDo.Extensions;
public class PasswordExtensions
{
    //propa附加属性，让PasswordBox支持绑定
    public static string GetPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(PasswordProperty);
    }
    public static void SetPassword(DependencyObject obj, string value)
    {
        obj.SetValue(PasswordProperty, value);
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordExtensions), new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));
    /// <summary>
    /// 依赖属性变更时的操作
    /// </summary>
    /// <param name="sender">依赖对象</param>
    /// <param name="e">事件接收对象</param>
    static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var passwordBox = sender as PasswordBox;
        string passwordStr = (string)e.NewValue;
        if (passwordBox != null && passwordBox.Password!=passwordStr)
        {
            passwordBox.Password=passwordStr;//重新给PasswordBox的Password属性赋值
        }
    }
}