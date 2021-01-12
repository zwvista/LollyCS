using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LollyCommon;
using ReactiveUI;
using System.Reactive.Linq;

namespace LollyXamarin.Views
{
    public partial class SearchPage : ContentPage
    {
        SettingsViewModel vm = AppShell.vmSettings;
        public DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        public MDictionary Dict;
        public string Word = "";
        public string Url;

        public SearchPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetData();
            BindingContext = vm;
            vm.WhenAnyValue(x => x.SelectedDictReference).Where(v => v != null).Subscribe(async v =>
            {
                Dict = v;
                dictStatus = DictWebBrowserStatus.Ready;
                Url = Dict.UrlString(Word, vm.AutoCorrects.ToList());
                if (Dict.DICTTYPENAME == "OFFLINE")
                {
                    wbDict.Source = "about:blank";
                    var html = await vm.client.GetStringAsync(Url);
                    var str = Dict.HtmlString(html, Word);
                    wbDict.Source = str;
                }
                else
                {
                    wbDict.Source = Url;
                    if (Dict.AUTOMATION != null)
                        dictStatus = DictWebBrowserStatus.Automating;
                    else if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                }
            });
        }

        async void wbDict_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (dictStatus == DictWebBrowserStatus.Ready) return;
            switch (dictStatus)
            {
                case DictWebBrowserStatus.Automating:
                    var s = Dict.AUTOMATION.Replace("{0}", Word);
                    await wbDict.EvaluateJavaScriptAsync(s);
                    dictStatus = DictWebBrowserStatus.Ready;
                    if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                    break;
                case DictWebBrowserStatus.Navigating:
                    var html = await wbDict.EvaluateJavaScriptAsync("document.body.innerHTML");
                    var str = Dict.HtmlString(html, Word);
                    dictStatus = DictWebBrowserStatus.Ready;
                    wbDict.Source = str;
                    break;
            }
        }
    }
}