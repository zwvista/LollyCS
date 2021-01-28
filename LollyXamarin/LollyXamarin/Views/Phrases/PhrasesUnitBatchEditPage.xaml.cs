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
    public partial class PhrasesUnitBatchEditPage : ContentPage
    {
        PhrasesUnitBatchEditViewModel vmBatch;

        public PhrasesUnitBatchEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmBatch = (PhrasesUnitBatchEditViewModel)BindingContext;
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();

        void OnSave(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}