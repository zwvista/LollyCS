using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class PhrasesUnitBatchEditPage : ContentPage
    {
        PhrasesUnitBatchEditViewModel vmBatch = null!;

        public PhrasesUnitBatchEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmBatch = (PhrasesUnitBatchEditViewModel)BindingContext;
        }

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitPhrase)((TappedEventArgs)e).Parameter;
            item.IsChecked = !item.IsChecked;
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();

        void OnSave(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}