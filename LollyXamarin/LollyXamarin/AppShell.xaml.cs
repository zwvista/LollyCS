using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LollyCommon;
using LollyXamarin.ViewModels;
using LollyXamarin.Views;
using Xamarin.Forms;

namespace LollyXamarin
{
    public partial class AppShell : Shell
    {
        public static SettingsViewModel vmSettings = new SettingsViewModel();
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(WordsUnitDetailPage), typeof(WordsUnitDetailPage));
            Routing.RegisterRoute(nameof(WordsTextbookDetailPage), typeof(WordsTextbookDetailPage));
            Routing.RegisterRoute(nameof(WordsLangDetailPage), typeof(WordsLangDetailPage));
            Routing.RegisterRoute(nameof(PhrasesUnitDetailPage), typeof(PhrasesUnitDetailPage));
            Routing.RegisterRoute(nameof(PhrasesTextbookDetailPage), typeof(PhrasesTextbookDetailPage));
            Routing.RegisterRoute(nameof(PhrasesLangDetailPage), typeof(PhrasesLangDetailPage));
            Routing.RegisterRoute(nameof(PatternsDetailPage), typeof(PatternsDetailPage));

            Task.Run(() => vmSettings.GetData()).Wait();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
