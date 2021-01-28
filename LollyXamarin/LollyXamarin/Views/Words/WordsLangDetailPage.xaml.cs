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
    public partial class WordsLangDetailPage : ContentPage
    {
        WordsLangDetailViewModel vmDetail;

        public WordsLangDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmDetail = (WordsLangDetailViewModel)BindingContext;
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