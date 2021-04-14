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
    public partial class PhrasesUnitPage : ContentPage
    {
        PhrasesUnitViewModel vm;

        public PhrasesUnitPage()
        {
            InitializeComponent();
            BindingContext = vm = new PhrasesUnitViewModel(AppShell.vmSettings, true, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MUnitPhrase item) =>
            await Shell.Current.GoToModalAsync(nameof(PhrasesUnitDetailPage), new PhrasesUnitDetailViewModel(vm, item, 0));

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitPhrase)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MUnitPhrase)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MUnitPhrase)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Copy Phrase", "Google Phrase");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    await Edit(item);
                    break;
                case "Copy Phrase":
                    CrossClipboard.Current.SetText(item.PHRASE);
                    break;
                case "Google Phrase":
                    await item.PHRASE.GoogleXamarin();
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }

        async void ToolbarItemMore_Clicked(object sender, EventArgs e)
        {
            var a = await DisplayActionSheet("More", "Cancel", null, "Add", "Batch Edit");
            switch (a)
            {
                case "Add":
                    await Shell.Current.GoToModalAsync(nameof(PhrasesUnitDetailPage), new PhrasesUnitDetailViewModel(vm, vm.NewUnitPhrase(), 0));
                    break;
                case "Batch Edit":
                    await Shell.Current.GoToModalAsync(nameof(PhrasesUnitBatchEditPage), new PhrasesUnitBatchEditViewModel(vm));
                    break;
            }
        }
    }
}