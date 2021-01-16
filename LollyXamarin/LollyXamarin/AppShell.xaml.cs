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

            Task.Run(() => vmSettings.GetData()).Wait();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
