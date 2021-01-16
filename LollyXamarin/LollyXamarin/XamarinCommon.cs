using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LollyXamarin
{
    public static class XamarinCommon
    {
        public static async Task GoogleXamarin(this string str) =>
            await Browser.OpenAsync($"https://www.google.com/search?q={HttpUtility.UrlEncode(str)}");
    }

    public interface IPageNavigate
    {
        void OnPageNavigated(object navigationData);
    }
    // https://stackoverflow.com/questions/57554375/xamarin-forms-4-shell-navigation-with-complex-parameters
    public static class ShellExtensions
    {
        public static async Task GoToAsync(this Shell shell, ShellNavigationState state, object navigationData, bool animate = false)
        {
            // https://stackoverflow.com/questions/2051357/adding-and-removing-anonymous-event-handler/30763657
            EventHandler<ShellNavigatedEventArgs> handler = null;
            handler = (sender, e) =>
            {
                ((Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage
                as IPageNavigate)?.OnPageNavigated(navigationData);
                shell.Navigated -= handler;
            };
            shell.Navigated += handler;
            await shell.GoToAsync(state, animate);
        }
    }
}
