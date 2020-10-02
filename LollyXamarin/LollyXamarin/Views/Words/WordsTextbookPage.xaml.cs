using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LollyCommon;

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
    }
}