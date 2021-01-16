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
using Xamarin.Essentials;

namespace LollyXamarin.Views
{
    public partial class WordsUnitPage : ContentPage
    {
        WordsUnitViewModel vm;

        public WordsUnitPage()
        {
            InitializeComponent();
            BindingContext = vm = new WordsUnitViewModel(AppShell.vmSettings, true, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void Edit(MUnitWord item) =>
            Navigation.PushAsync(new WordsUnitDetailPage
            {
                BindingContext = new WordsUnitDetailViewModel(vm, item, 0),
            });

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitWord)((TappedEventArgs)e).Parameter;
            Edit(item);
        }

        void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MUnitWord)((SwipeItem)sender).BindingContext;
            Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            // https://forums.xamarin.com/discussion/180866/how-to-set-swipeview-items-from-staticresource
            var item = (MUnitWord)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Retrieve Note", "Clear Note", "Copy Word", "Google Word", "Online Dictionary");
            switch(a)
            {
                case "Delete":
                    break;
                case "Edit":
                    Edit(item);
                    break;
                case "Retrieve Note":
                    break;
                case "Clear Note":
                    break;
                case "Copy Word":
                    // https://forums.xamarin.com/discussion/126579/how-to-copy-a-value-to-clipboard
                    CrossClipboard.Current.SetText(item.WORD);
                    break;
                case "Google Word":
                    await item.WORD.GoogleXamarin();
                    break;
                case "Online Dictionary":
                    var url = vm.vmSettings.SelectedDictReference.UrlString(item.WORD, vm.vmSettings.AutoCorrects);
                    await Launcher.OpenAsync(new Uri(url));
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }
    }
}