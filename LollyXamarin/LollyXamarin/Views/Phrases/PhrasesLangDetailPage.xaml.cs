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
    public partial class PhrasesLangDetailPage : ContentPage
    {
        PhrasesLangDetailViewModel vmDetail;

        public PhrasesLangDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmDetail = (PhrasesLangDetailViewModel)BindingContext;
            BindingContext = vmDetail.ItemEdit;
        }
    }
}