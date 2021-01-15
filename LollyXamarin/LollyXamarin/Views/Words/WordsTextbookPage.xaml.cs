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
    public partial class WordsTextbookPage : ContentPage
    {
        WordsUnitViewModel vm;

        public WordsTextbookPage()
        {
            InitializeComponent();
            BindingContext = vm = new WordsUnitViewModel(AppShell.vmSettings, false, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitWord)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new WordsTextbookDetailPage
            {
                BindingContext = new WordsUnitDetailViewModel(vm, item, 0),
            });
        }

        void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MUnitWord)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Retrieve Note", "Clear Note", "Copy Word", "Google Word", "Online Dictionary");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    break;
                case "Retrieve Note":
                    break;
                case "Clear Note":
                    break;
                case "Copy Word":
                    CrossClipboard.Current.SetText(item.WORD);
                    break;
                case "Google Word":
                    break;
                case "Online Dictionary":
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }
    }
}