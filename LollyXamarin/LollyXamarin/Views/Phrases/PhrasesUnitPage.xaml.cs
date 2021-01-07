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
    public partial class PhrasesUnitPage : ContentPage
    {
        PhrasesUnitViewModel vm;

        public PhrasesUnitPage()
        {
            InitializeComponent();
            BindingContext = vm = new PhrasesUnitViewModel(AppShell.vmSettings, true, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var item = (MUnitPhrase)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new PhrasesUnitDetailPage
            {
                BindingContext = new PhrasesUnitDetailViewModel(vm, item, 0),
            });
        }
    }
}