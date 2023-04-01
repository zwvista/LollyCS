using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace LollyXamarin
{
    public partial class SearchPage : ContentPage, IOnlineDict
    {
        SearchViewModel vm;

        public SearchPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            vm = new SearchViewModel(AppShell.vmSettings, this);
            await vm.vmSettings.GetData();
            BindingContext = vm;
        }

        async void wbDict_Navigated(object sender, WebNavigatedEventArgs e)
        {
            await vm.vmDict.OnNavigationFinished();
        }

        public void LoadURL(string url) =>
            wbDict.Source = url;

        public void LoadHtml(string html) =>
            wbDict.Source = html;

        public async Task EvaluateScriptAsync(string javascript) =>
            await wbDict.EvaluateJavaScriptAsync(javascript);

        public async Task<string> GetSourceAsync() =>
            await wbDict.EvaluateJavaScriptAsync("document.body.innerHTML");
    }
}