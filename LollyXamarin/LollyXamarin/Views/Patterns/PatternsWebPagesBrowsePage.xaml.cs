using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;
using ReactiveUI;
using System.Reactive.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace LollyXamarin
{
    public partial class PatternsWebPagesBrowsePage : ContentPage, IPageNavigate
    {
        PatternsWebPagesViewModel vm;

        public PatternsWebPagesBrowsePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            BindingContext = vm = (PatternsWebPagesViewModel)navigationData;
        }

        void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vm == null) return;
            wbWebPage.Source = vm.SelectedWebPageItem.URL;
        }
    }
}