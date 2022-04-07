using System.Windows;
using MyToDo.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace MyToDo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
    }
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
            
    }
}