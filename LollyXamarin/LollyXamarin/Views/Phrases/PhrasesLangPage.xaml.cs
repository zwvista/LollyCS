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

        void Edit(MLangPhrase item) =>
            Navigation.PushAsync(new PhrasesLangDetailPage
            {
                BindingContext = new PhrasesLangDetailViewModel(vm, item),
            });

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangPhrase)((TappedEventArgs)e).Parameter;
            Edit(item);
        }

        void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangPhrase)((SwipeItem)sender).BindingContext;
            Edit(item);
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
                    Edit(item);
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
    }
}