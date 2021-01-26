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
    public partial class PhrasesUnitBatchEditPage : ContentPage, IPageNavigate
    {
        PhrasesUnitBatchEditViewModel vmBatch;

        public PhrasesUnitBatchEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            BindingContext = vmBatch = (PhrasesUnitBatchEditViewModel)navigationData;
        }
    }
}