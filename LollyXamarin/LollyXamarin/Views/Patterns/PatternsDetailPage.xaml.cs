using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LollyCommon;

namespace LollyXamarin
{
    public partial class PatternsDetailPage : ContentPage
    {
        PatternsDetailViewModel vmDetail;

        public PatternsDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmDetail = (PatternsDetailViewModel)BindingContext;
            BindingContext = vmDetail.ItemEdit;
            vmDetail.ItemEdit.Save.WhenFinishedExecuting().Subscribe(_ => Navigation.PopModalAsync());
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();
    }
}