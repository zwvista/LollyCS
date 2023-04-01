using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LollyCommon;
using Plugin.Clipboard;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace LollyXamarin
{
    public partial class PhrasesLangPage : ContentPage
    {
        PhrasesLangViewModel vm;

        public PhrasesLangPage()
        {
            InitializeComponent();
            BindingContext = vm = new PhrasesLangViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MLangPhrase item) =>
            await Shell.Current.GoToModalAsync(nameof(PhrasesLangDetailPage), new PhrasesLangDetailViewModel(vm, item));

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangPhrase)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangPhrase)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangPhrase)((SwipeItem)sender).BindingContext;
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

        async void ToolbarItemAdd_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToModalAsync(nameof(PhrasesLangDetailPage), new PhrasesLangDetailViewModel(vm, vm.NewLangPhrase()));

        }
    }
}