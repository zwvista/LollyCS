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
    public partial class PhrasesReviewPage : ContentPage
    {
        PhrasesReviewViewModel vm = new PhrasesReviewViewModel(AppShell.vmSettings, false, () =>
        {
        });

        public PhrasesReviewPage()
        {
            InitializeComponent();
            vm = new PhrasesReviewViewModel(AppShell.vmSettings, false, async () =>
            {
                PhraseInputEntry.Focus();
                if (vm.HasNext && vm.IsSpeaking)
                    await XamarinCommon.SpeakXamarin(AppShell.vmSettings, vm.CurrentPhrase);
            });
            OnNewTest(null, null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnNewTest(object sender, EventArgs e)
        {
            await Shell.Current.GoToModalAsync(nameof(ReviewOptionsPage), vm.Options);
        }

        void OnCheck(object sender, EventArgs e) =>
            vm.Check();
    }
}