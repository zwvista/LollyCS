using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LollyCommon;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LollyXamarin
{
    public static class XamarinCommon
    {
        public static async Task GoogleXamarin(this string str) =>
            await Browser.OpenAsync($"https://www.google.com/search?q={HttpUtility.UrlEncode(str)}");
        public static async Task SpeakXamarin(this SettingsViewModel vmSettings, string text)
        {
            var locales = await TextToSpeech.GetLocalesAsync();
            var local = locales.FirstOrDefault(o => vmSettings.Voices.First(o2 => o2.VOICETYPEID == 4).VOICELANG.StartsWith(o.Language));
            await TextToSpeech.SpeakAsync(text, new SpeechOptions
            {
                Locale = local
            });
        }
    }

    public interface IPageNavigate
    {
        void OnPageNavigated(object navigationData);
    }
    public static class ShellExtensions
    {
        // https://stackoverflow.com/questions/57554375/xamarin-forms-4-shell-navigation-with-complex-parameters
        public static async Task GoToAsync(this Shell shell, ShellNavigationState state, object navigationData, bool animate = false)
        {
            // https://stackoverflow.com/questions/2051357/adding-and-removing-anonymous-event-handler/30763657
            void handler(object sender, EventArgs e)
            {
                ((Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage
                as IPageNavigate)?.OnPageNavigated(navigationData);
                shell.Navigated -= handler;
            }
            shell.Navigated += handler;
            await shell.GoToAsync(state, animate);
        }
        // https://forums.xamarin.com/discussion/168512/shell-showing-modal-pages
        public static Task GoToModalAsync(this Shell shell, string route, object navigationData)
        {
            var page = Routing.GetOrCreateContent(route) as Page;
            if (page is null) return Task.CompletedTask;
            page.BindingContext = navigationData;
            return shell.Navigation.PushModalAsync(new NavigationPage(page));
        }
    }
}
