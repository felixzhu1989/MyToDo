using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Navigation;

namespace MyToDo.Views;

/// <summary>
/// AboutView.xaml 的交互逻辑
/// </summary>
public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
    }
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        // for .NET Core you need to add UseShellExecute = true
        // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
        var startInfo = new ProcessStartInfo(e.Uri.AbsoluteUri)
        { UseShellExecute =true };
        Process.Start(startInfo);
        e.Handled = true;

    }
}