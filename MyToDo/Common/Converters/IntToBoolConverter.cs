using System;
using System.Globalization;
using System.Windows.Data;
namespace MyToDo.Common.Converters;
/// <summary>
/// Int转换成Bool类型
/// </summary>
public class IntToBoolConverter : IValueConverter
{
    //后台传给UI
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value !=null && int.TryParse(value.ToString(), out int result))
        {
            if (result == 0) return false;
        }
        return true;
    }
    //UI传给后台
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value !=null && bool.TryParse(value.ToString(), out bool result))
        {
            if (result) return 1;
        }
        return 0;
    }
}
