using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels;
public class MsgViewModel : BindableBase, IDialogHostAware
{
    public string DialogHostName { get; set; } = "RootDialog";
    public DelegateCommand SaveCommand { get ; set ; }
    public DelegateCommand CancelCommand { get ; set ; }
    private string title;
    public string Title
    {
        get => title;
        set { title = value;RaisePropertyChanged(); }
    }
    private string content;
    public string Content
    {
        get => content;
        set { content = value; RaisePropertyChanged(); }
    }

    public MsgViewModel()
    {
        SaveCommand=new DelegateCommand(Save);
        CancelCommand=new DelegateCommand(Cancel);
    }
    private void Cancel()
    {
        if (DialogHost.IsDialogOpen(DialogHostName))
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
    }
    private void Save()
    {
        if (DialogHost.IsDialogOpen(DialogHostName))
        {
            DialogParameters param = new DialogParameters();
            //保存时传递参数
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
        }
    }
    public void OnDialogOpen(IDialogParameters parameters)
    {
        if(parameters.ContainsKey("Title"))
            Title=parameters.GetValue<string>("Title");
        if (parameters.ContainsKey("Content"))
            Content=parameters.GetValue<string>("Content");
    }
}
