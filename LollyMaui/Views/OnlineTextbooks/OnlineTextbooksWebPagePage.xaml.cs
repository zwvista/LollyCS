using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;
using ReactiveUI;
using System.Reactive.Linq;

namespace LollyMaui
{
    public partial class OnlineTextbooksWebPagePage : ContentPage, IPageNavigate
    {
        OnlineTextbooksWebPageViewModel vm = null!;

        public OnlineTextbooksWebPagePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            vm = (OnlineTextbooksWebPageViewModel)navigationData;
            BindingContext = vm;
        }

        void WebView_SwipedLeft(object sender, SwipedEventArgs e) =>
            vm.Next(-1);

        void WebView_SwipedRight(object sender, SwipedEventArgs e) =>
            vm.Next(1);
    }
}