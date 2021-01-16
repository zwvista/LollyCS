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
    public partial class PatternsWebPagesListPage : ContentPage, IPageNavigate
    {
        PatternsWebPagesViewModel vm;

        public PatternsWebPagesListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            BindingContext = vm = (PatternsWebPagesViewModel)navigationData;
        }

        async Task Edit(MPatternWebPage item) =>
            await Shell.Current.GoToAsync(nameof(PatternsWebPagesDetailPage), new PatternsWebPagesDetailViewModel(vm, item));

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MPatternWebPage)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MPatternWebPage)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MPatternWebPage)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    await Edit(item);
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }
    }
}