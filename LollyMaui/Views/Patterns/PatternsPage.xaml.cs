using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class PatternsPage : ContentPage
    {
        PatternsViewModel vm;

        public PatternsPage()
        {
            InitializeComponent();
            BindingContext = vm = new PatternsViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MPattern item) =>
            await Shell.Current.GoToModalAsync(nameof(PatternsDetailPage), new PatternsDetailViewModel(vm, item));

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MPattern)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MPattern)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MPattern)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Edit", "Browse Web Page", "Copy Pattern", "Google Pattern");
            switch (a)
            {
                case "Edit":
                    await Edit(item);
                    break;
                case "Browse Web Page":
                    await Shell.Current.GoToAsync(nameof(PatternsWebPagePage), new PatternsDetailViewModel(vm, item));
                    break;
                case "Copy Pattern":
                    await Clipboard.Default.SetTextAsync(item.PATTERN);
                    break;
                case "Google Pattern":
                    await item.PATTERN.GoogleMaui();
                    break;
            }
        }

        async void ToolbarItemAdd_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToModalAsync(nameof(PatternsDetailPage), new PatternsDetailViewModel(vm, vm.NewPattern()));
        }
    }
}