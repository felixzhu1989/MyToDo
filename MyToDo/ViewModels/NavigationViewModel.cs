using MyToDo.Extensions;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels;

public class NavigationViewModel:BindableBase,INavigationAware
{
    private readonly IContainerProvider _containerProvider;
    //事件聚合器
    public readonly IEventAggregator _aggregator;

    public NavigationViewModel(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
        _aggregator = containerProvider.Resolve<IEventAggregator>();
    }
    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;//是否重用以前的窗口
    }
    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
    }
    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public void UpdateLoading(bool IsOpen)
    {
        _aggregator.UpdateLoading(new Common.Events.UpdateModel{IsOpen = IsOpen});
    }
}