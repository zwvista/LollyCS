using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LollyCommon;
using Plugin.Clipboard;

namespace LollyXamarin.Views
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
            await Shell.Current.GoToAsync(nameof(PatternsDetailPage), new PatternsDetailViewModel(vm, item));

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
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Browse Web Pages", "Edit Web Pages", "Copy Pattern", "Google Pattern");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    await Edit(item);
                    break;
                case "Browse Web Pages":
                    await Shell.Current.GoToAsync(nameof(PatternsWebPagesBrowsePage), new PatternsWebPagesViewModel(vm.vmSettings, false, item));
                    break;
                case "Edit Web Pages":
                    await Shell.Current.GoToAsync(nameof(PatternsWebPagesListPage), new PatternsWebPagesViewModel(vm.vmSettings, false, item));
                    break;
                case "Copy Pattern":
                    CrossClipboard.Current.SetText(item.PATTERN);
                    break;
                case "Google Pattern":
                    await item.PATTERN.GoogleXamarin();
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }

        async void ToolbarItemAdd_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PatternsDetailPage), new PatternsDetailViewModel(vm, vm.NewPattern()));
        }
    }
}