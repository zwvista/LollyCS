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
        MOnlineTextbook vmDetail = null!;

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
            vmDetail = (MOnlineTextbook)navigationData;
            wbWebPage.Source = vmDetail.URL;
        }
    }
}