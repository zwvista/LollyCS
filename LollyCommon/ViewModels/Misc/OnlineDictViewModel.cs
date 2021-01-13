using System;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon.ViewModels
{
    public interface IOnlineDict
    {
        void LoadURL(string url);
        void LoadHtml(string html);
        Task ExecuteJavaScriptAsync(string javascript);
        Task<string> GetSourceAsync();
    }

    public class OnlineDictViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; set; }
        public IOnlineDict OnlineDict { get; set; }
        DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        public MDictionary Dict;
        [Reactive]
        public string Word { get; set; } = "";
        [Reactive]
        public string Url { get; set; } = "";

        public OnlineDictViewModel(SettingsViewModel vmSettings, IOnlineDict dict)
        {
            this.vmSettings = vmSettings;
            OnlineDict = dict;
        }

        public async Task SearchDict()
        {
            dictStatus = DictWebBrowserStatus.Ready;
            Url = Dict.UrlString(Word, vmSettings.AutoCorrects.ToList());
            if (Dict.DICTTYPENAME == "OFFLINE")
            {
                OnlineDict.LoadURL("about:blank");
                var html = await vmSettings.client.GetStringAsync(Url);
                var str = Dict.HtmlString(html, Word);
                OnlineDict.LoadHtml(str);
            }
            else
            {
                OnlineDict.LoadURL(Url);
                if (Dict.AUTOMATION != null)
                    dictStatus = DictWebBrowserStatus.Automating;
                else if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                    dictStatus = DictWebBrowserStatus.Navigating;
            }

        }

        public async Task OnNavigationFinished()
        {
            if (dictStatus == DictWebBrowserStatus.Ready) return;
            switch (dictStatus)
            {
                case DictWebBrowserStatus.Automating:
                    var s = Dict.AUTOMATION.Replace("{0}", Word);
                    await OnlineDict.ExecuteJavaScriptAsync(s);
                    dictStatus = DictWebBrowserStatus.Ready;
                    if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                    break;
                case DictWebBrowserStatus.Navigating:
                    var html = await OnlineDict.GetSourceAsync();
                    var str = Dict.HtmlString(html, Word);
                    dictStatus = DictWebBrowserStatus.Ready;
                    OnlineDict.LoadHtml(str);
                    break;
            }
        }
    }
}
