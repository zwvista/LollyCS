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
    public partial class WordsUnitBatchEditPage : ContentPage, IPageNavigate
    {
        WordsUnitBatchEditViewModel vmBatch;

        public WordsUnitBatchEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            BindingContext = vmBatch = (WordsUnitBatchEditViewModel)navigationData;
        }

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitWord)((TappedEventArgs)e).Parameter;
            item.IsChecked = !item.IsChecked;
        }
    }
}