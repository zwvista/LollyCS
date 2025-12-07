using System;
using System.Collections;
using LollyCommon;

namespace LollyMaui
{
    public partial class PatternsPage : ContentPage
    {
        PatternsViewModel vm;

        public PatternsPage()
        {
            InitializeComponent();
            BindingContext = vm = new PatternsViewModel(AppShell.vmSettings, false, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MPattern item) =>
            await Shell.Current.GoToModalAsync(nameof(PatternsDetailPage), new PatternsDetailViewModel(vm, item));

        async Task BrowseWebPage(MPattern item)
        {
            var index = vm.PatternItems.IndexOf(item);
            var (start, end) = CommonApi.GetPreferredRangeFromArray(index, vm.PatternItems.Count, 50);
            var items = vm.PatternItems.ToList().Slice(start, end);
            await Shell.Current.GoToAsync(nameof(PatternsWebPagePage), new PatternsWebPageViewModel(items, index));
        }

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
                    await BrowseWebPage(item);
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

        async void IconButton_Clicked(object sender, EventArgs e)
        {
            var item = (MPattern)((ImageButton)sender).BindingContext;
            await BrowseWebPage(item);
        }
    }
}