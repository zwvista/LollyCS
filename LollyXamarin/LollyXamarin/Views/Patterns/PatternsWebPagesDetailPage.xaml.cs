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
    public partial class PatternsWebPagesDetailPage : ContentPage, IPageNavigate
    {
        PatternsDetailViewModel vmDetail;

        public PatternsWebPagesDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            vmDetail = (PatternsDetailViewModel)navigationData;
            BindingContext = vmDetail.ItemEdit;
        }
    }
}