using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelpCentreUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            Routine.Ip.AddListener(textBlock);
            Routine.Ip.StartUpdates();
        }

        private void CopyIpTap(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Routine.General.CopyTextToClipboard(Routine.Ip.ExternalIp);
            _ = MinimizeApp();
        }

        private async Task MinimizeApp()
        {
            IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
            await resourceInfos[0].StartSuspendAsync();
        }

        private void ContentControl_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ContentControl, "PointerOver", false);
        }

        private void ContentControl_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ContentControl, "Normal", false);
        }
    }
}
