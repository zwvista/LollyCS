using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LollyCloud;

namespace LollyXamarin.Views
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
    }
}