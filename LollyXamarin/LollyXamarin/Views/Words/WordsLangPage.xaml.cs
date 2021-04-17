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

namespace LollyXamarin
{
    public partial class WordsLangPage : ContentPage
    {
        WordsLangViewModel vm;

        public WordsLangPage()
        {
            InitializeComponent();
            BindingContext = vm = new WordsLangViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MLangWord item) =>
            await Shell.Current.GoToModalAsync(nameof(WordsLangDetailPage), new WordsLangDetailViewModel(vm, item));

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangWord)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangWord)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangWord)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Retrieve Note", "Clear Note", "Copy Word", "Google Word", "Online Dictionary");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    await Edit(item);
                    break;
                case "Retrieve Note":
                    await vm.RetrieveNote(item);
                    break;
                case "Clear Note":
                    await vm.ClearNote(item);
                    break;
                case "Copy Word":
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

        async void ToolbarItemAdd_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToModalAsync(nameof(WordsLangDetailPage), new WordsLangDetailViewModel(vm, vm.NewLangWord()));
        }

        async void IconButton_Clicked(object sender, EventArgs e)
        {
            var words = vm.WordItems.Select(o => o.WORD).ToList();
            var item = (MLangWord)((Button)sender).BindingContext;
            int index = vm.WordItems.IndexOf(item);
            await Shell.Current.GoToAsync(nameof(WordsDictPage), new WordsDictViewModel(vm.vmSettings, words, index));
        }
    }
}