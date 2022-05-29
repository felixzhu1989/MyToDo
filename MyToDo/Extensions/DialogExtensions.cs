using System;
using System.Threading.Tasks;
using MyToDo.Common;
using MyToDo.Common.Events;
using Prism.Events;
using Prism.Services.Dialogs;

namespace MyToDo.Extensions;

public static class DialogExtensions
{
    /// <summary>
    /// 公共的询问窗口,扩展方法
    /// </summary>
    /// <param name="dialogHost">指定的dialoghost会话主机</param>
    /// <param name="title">标题</param>
    /// <param name="content">询问内容</param>
    /// <param name="dialogHostName">唯一的会话主机名称</param>
    /// <returns></returns>
    public static async Task<IDialogResult> Question(this IDialogHostService dialogHost, string title, string content, string dialogHostName = "RootDialog")
    {
        DialogParameters param = new DialogParameters
        {
            { "Title", title },
            { "Content", content },
            { "dialogHostName", dialogHostName }
        };
        var dialogResult = await dialogHost.ShowDialog("MsgView", param, dialogHostName);
        return dialogResult;
    }
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
    public static void Register(this IEventAggregator aggregator, Action<UpdateModel> action)
    {
        aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
    }
}