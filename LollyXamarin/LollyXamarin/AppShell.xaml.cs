using System;
using System.Collections.Generic;
using LollyXamarin.ViewModels;
using LollyXamarin.Views;
using Xamarin.Forms;

namespace LollyXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            // https://stackoverflow.com/questions/56748966/hide-tabbar-in-xamarin-forms-shell
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
