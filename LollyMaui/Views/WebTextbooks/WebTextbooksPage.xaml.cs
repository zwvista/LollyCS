using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class WebTextbooksPage : ContentPage
    {
        WebTextbooksViewModel vm;

        public WebTextbooksPage()
        {
            InitializeComponent();
            BindingContext = vm = new WebTextbooksViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MWebTextbook item) =>
            await Shell.Current.GoToModalAsync(nameof(WebTextbooksDetailPage), item);

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MWebTextbook)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MWebTextbook)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MWebTextbook)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Edit", "Browse Web Page");
            switch (a)
            {
                case "Edit":
                    await Edit(item);
                    break;
                case "Browse Web Page":
                    await Shell.Current.GoToAsync(nameof(WebTextbooksWebPagePage), item);
                    break;
            }
        }
    }
}