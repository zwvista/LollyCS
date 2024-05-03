using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel vm = AppShell.vmSettings;

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetData();
            BindingContext = vm;
        }
    }
}