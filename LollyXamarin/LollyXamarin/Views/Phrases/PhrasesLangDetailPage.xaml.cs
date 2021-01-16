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
    public partial class PhrasesLangDetailPage : ContentPage, IPageNavigate
    {
        PhrasesLangDetailViewModel vmDetail;

        public PhrasesLangDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            vmDetail = (PhrasesLangDetailViewModel)navigationData;
            BindingContext = vmDetail.ItemEdit;
        }
    }
}