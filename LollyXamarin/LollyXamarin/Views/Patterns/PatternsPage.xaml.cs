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
    public partial class PatternsPage : ContentPage
    {
        PatternsViewModel vm;

        public PatternsPage()
        {
            InitializeComponent();
            BindingContext = vm = new PatternsViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}