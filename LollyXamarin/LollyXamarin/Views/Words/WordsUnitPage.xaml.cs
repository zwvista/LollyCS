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

        void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var item = (MUnitWord)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new WordsUnitDetailPage
            {
                BindingContext = new WordsUnitDetailViewModel(vm, item, 0),
            });
        }
    }
}