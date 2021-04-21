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
    public partial class WordsReviewPage : ContentPage
    {
        WordsReviewViewModel vm = new WordsReviewViewModel();
        public WordsReviewPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Shell.Current.GoToModalAsync(nameof(ReviewOptionsPage), vm.Options);
        }

        void NewTest_Clicked(object sender, EventArgs e)
        {
        }
    }
}