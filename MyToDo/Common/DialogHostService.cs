using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo.Common;
/// <summary>
/// 对话主机服务
/// </summary>
public class DialogHostService : DialogService, IDialogHostService
{
    private readonly IContainerExtension containerExtension;
    public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
    {
        this.containerExtension=containerExtension;
    }
    public async Task<IDialogResult?> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "RootDialog")
    {

        if (parameters == null)
            parameters = new DialogParameters();
        //从容器当中取出弹出窗口的实例
        var content = containerExtension.Resolve<object>(name);
        //验证实例的有效性
        if (!(content is FrameworkElement dialogContent))
            throw new NullReferenceException("A dialog's content must be a FrameworkElement");
        if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            ViewModelLocator.SetAutoWireViewModel(view, true);
        if (!(dialogContent.DataContext is IDialogHostAware viewModel))
            throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");
        //设置顶级节点名称
        viewModel.DialogHostName = dialogHostName;
        //MaterialDesign提供的事件
        DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
        {
            if (viewModel is IDialogHostAware aware)
            {
                aware.OnDialogOpen(parameters);//传递参数
            }
            eventArgs.Session.UpdateContent(content);
        };
        var dialogResult = await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        return dialogResult as IDialogResult;
       // return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
    }
}

