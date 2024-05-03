using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class PhrasesLangDetailPage : ContentPage
    {
        PhrasesLangDetailViewModel vmDetail = null!;

        public PhrasesLangDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmDetail = (PhrasesLangDetailViewModel)BindingContext;
            BindingContext = vmDetail.ItemEdit;
            vmDetail.ItemEdit.Save.WhenFinishedExecuting().Subscribe(_ => Navigation.PopModalAsync());
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();
    }
}