using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
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
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Get Note", "Clear Note", "Copy Word", "Google Word", "Online Dictionary");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    await Edit(item);
                    break;
                case "Get Note":
                    await vm.GetNote(item);
                    break;
                case "Clear Note":
                    await vm.ClearNote(item);
                    break;
                case "Copy Word":
                    await Clipboard.Default.SetTextAsync(item.WORD);
                    break;
                case "Google Word":
                    await item.WORD.GoogleMaui();
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
            var item = (MLangWord)((ImageButton)sender).BindingContext;
            int index = vm.WordItems.IndexOf(item);
            var (start, end) = CommonApi.GetPreferredRangeFromArray(index, vm.WordItems.Count, 50);
            var words = vm.WordItems.Select(o => o.WORD).ToList().Slice(start, end);
            await Shell.Current.GoToAsync(nameof(WordsDictPage), new WordsDictViewModel(vm.vmSettings, words, index));
        }
    }
}