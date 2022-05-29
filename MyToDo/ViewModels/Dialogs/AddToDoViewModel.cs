using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels.Dialogs;
public class AddToDoViewModel : IDialogHostAware
{
    public string DialogHostName { get; set; }
    public DelegateCommand SaveCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }
    public AddToDoViewModel()
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

    }
}
