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

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MPattern)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new PatternsDetailPage
            {
                BindingContext = new PatternsDetailViewModel(vm, item),
            });
        }

        void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
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
                    break;
                case "Browse Web Pages":
                    break;
                case "Edit Web Pages":
                    break;
                case "Copy Pattern":
                    CrossClipboard.Current.SetText(item.PATTERN);
                    break;
                case "Google Pattern":
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }
    }
}