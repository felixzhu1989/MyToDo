using System;
using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;

namespace MyToDo.ViewModels;

public class SkinViewModel:BindableBase
{
    private bool _isDarkTheme;
    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
            {
                ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
            }
        }
    }
    public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;
    public DelegateCommand<object> ChangeHueCommand { get; }
    private readonly PaletteHelper _paletteHelper = new PaletteHelper();

    public SkinViewModel()
    {
        ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
    }
    private void ChangeHue(object? obj)
    {
        var hue = (Color)obj!;
        ITheme theme = _paletteHelper.GetTheme();
        theme.PrimaryLight = new ColorPair(hue.Lighten());
        theme.PrimaryMid = new ColorPair(hue);
        theme.PrimaryDark = new ColorPair(hue.Darken());
        _paletteHelper.SetTheme(theme);
    }
    private static void ModifyTheme(Action<ITheme> modificationAction)
    {
        var paletteHelper = new PaletteHelper();
        ITheme theme = paletteHelper.GetTheme();

        modificationAction?.Invoke(theme);

        paletteHelper.SetTheme(theme);
    }
}