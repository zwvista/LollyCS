using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
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

        async Task Edit(MUnitWord item) =>
            await Shell.Current.GoToModalAsync(nameof(WordsUnitDetailPage), new WordsUnitDetailViewModel(vm, item, 0));

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitWord)((TappedEventArgs)e).Parameter;
            await vm.vmSettings.SpeakMaui(item.WORD);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MUnitWord)((SwipeItem)sender).BindingContext;
            await Edit(item);
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
                    await Edit(item);
                    break;
                case "Retrieve Note":
                    await vm.RetrieveNote(item);
                    break;
                case "Clear Note":
                    await vm.ClearNote(item);
                    break;
                case "Copy Word":
                    // https://forums.xamarin.com/discussion/126579/how-to-copy-a-value-to-clipboard
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

        async void ToolbarItemMore_Clicked(object sender, EventArgs e)
        {
            var a = await DisplayActionSheet("More", "Cancel", null, "Add", "Retrieve All Notes", "Retrieve Notes If Empty", "Clear All Notes", "Clear Notes If Empty", "Batch Edit");
            switch (a)
            {
                case "Add":
                    await Shell.Current.GoToModalAsync(nameof(WordsUnitDetailPage), new WordsUnitDetailViewModel(vm, vm.NewUnitWord(), 0));
                    break;
                case "Retrieve All Notes":
                    vm.IfEmpty = false;
                    await vm.RetrieveNotes(_ => { });
                    break;
                case "Retrieve Notes If Empty":
                    vm.IfEmpty = true;
                    await vm.RetrieveNotes(_ => { });
                    break;
                case "Clear All Notes":
                    vm.IfEmpty = false;
                    await vm.ClearNotes(_ => { });
                    break;
                case "Clear Notes If Empty":
                    vm.IfEmpty = true;
                    await vm.ClearNotes(_ => { });
                    break;
                case "Batch Edit":
                    await Shell.Current.GoToModalAsync(nameof(WordsUnitBatchEditPage), new WordsUnitBatchEditViewModel(vm));
                    break;
            }
        }

        async void IconButton_Clicked(object sender, EventArgs e)
        {
            var words = vm.WordItems.Select(o => o.WORD).ToList();
            var item = (MUnitWord)((Button)sender).BindingContext;
            int index = vm.WordItems.IndexOf(item);
            await Shell.Current.GoToAsync(nameof(WordsDictPage), new WordsDictViewModel(vm.vmSettings, words, index));
        }
    }
}