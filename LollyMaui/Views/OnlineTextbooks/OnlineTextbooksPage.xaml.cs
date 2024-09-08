using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class OnlineTextbooksPage : ContentPage
    {
        OnlineTextbooksViewModel vm;

        public OnlineTextbooksPage()
        {
            InitializeComponent();
            BindingContext = vm = new OnlineTextbooksViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MOnlineTextbook item) =>
            await Shell.Current.GoToModalAsync(nameof(OnlineTextbooksDetailPage), item);

        async Task BrowseWebPage(MOnlineTextbook item)
        {
            var index = vm.Items.IndexOf(item);
            var (start, end) = CommonApi.GetPreferredRangeFromArray(index, vm.Items.Count, 50);
            var items = vm.Items.ToList().Slice(start, end);
            await Shell.Current.GoToAsync(nameof(OnlineTextbooksWebPagePage), new OnlineTextbooksWebPageViewModel(items, index));
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MOnlineTextbook)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MOnlineTextbook)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MOnlineTextbook)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Edit", "Browse Web Page");
            switch (a)
            {
                case "Edit":
                    await Edit(item);
                    break;
                case "Browse Web Page":
                    await BrowseWebPage(item);
                    break;
            }
        }

        async void IconButton_Clicked(object sender, EventArgs e)
        {
            var item = (MOnlineTextbook)((ImageButton)sender).BindingContext;
            await BrowseWebPage(item);
        }
    }
}