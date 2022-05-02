using System;
using MyToDo.Common.Events;
using Prism.Events;

namespace MyToDo.Extensions;

public static class DialogExtensions
{
    /// <summary>
    /// 推送等待消息
    /// </summary>
    /// <param name="aggregator"></param>
    /// <param name="model"></param>
    public static void UpdateLoading(this IEventAggregator aggregator, UpdateModel model)
    {
        aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
    }
    /// <summary>
    /// 注册等待消息，给MainView注册
    /// </summary>
    /// <param name="aggregator"></param>
    /// <param name="action"></param>
    public static void Register(this IEventAggregator aggregator,Action<UpdateModel>  action)
    {
        aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
    }
}