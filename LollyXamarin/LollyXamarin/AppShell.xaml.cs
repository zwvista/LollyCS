using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LollyCloud;
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
            // https://stackoverflow.com/questions/56748966/hide-tabbar-in-xamarin-forms-shell
            SetTabBarIsVisible(this, false);
            Task.Run(() => vmSettings.GetData()).Wait();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
