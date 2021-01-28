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
    public partial class PhrasesUnitDetailPage : ContentPage
    {
        PhrasesUnitDetailViewModel vmDetail;

        public PhrasesUnitDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmDetail = (PhrasesUnitDetailViewModel)BindingContext;
            BindingContext = vmDetail.ItemEdit;
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();

        void OnSave(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}