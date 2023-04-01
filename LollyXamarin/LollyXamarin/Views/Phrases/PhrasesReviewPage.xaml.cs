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
                if (vm.HasCurrent && vm.IsSpeaking)
                    await XamarinCommon.SpeakXamarin(AppShell.vmSettings, vm.CurrentPhrase);
            });
            BindingContext = vm;
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
            vm.Check(sender == btnCheckNext);
    }
}