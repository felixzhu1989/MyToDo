using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace MyToDo.ViewModels.Dialogs;
public class AddMemoViewModel : BindableBase, IDialogHostAware
{
    public string DialogHostName { get; set; }
    public DelegateCommand SaveCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }
    private MemoDto model;
    public MemoDto Model
    {
        get { return model; }
        set { model = value; RaisePropertyChanged(); }
    }
    public AddMemoViewModel()
    {
        SaveCommand=new DelegateCommand(Save);
        CancelCommand=new DelegateCommand(Cancel);
    }
    private void Cancel()
    {
        if (DialogHost.IsDialogOpen(DialogHostName))
            //取消时只返回No，告知操作结束
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
    }
    private void Save()
    {
        //验证数据已填写
        if (string.IsNullOrWhiteSpace(Model.Title)||string.IsNullOrWhiteSpace(model.Content)) return;
        if (DialogHost.IsDialogOpen(DialogHostName))
        {
            DialogParameters param = new() { { "Value", Model } };
            //保存时传递参数
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
        }
    }
    public void OnDialogOpen(IDialogParameters parameters)
    {
        if (parameters.ContainsKey("Value")) Model=parameters.GetValue<MemoDto>("Value");
        else Model=new();
    }
}
