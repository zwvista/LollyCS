﻿using System;
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
    public partial class WordsUnitBatchEditPage : ContentPage
    {
        WordsUnitBatchEditViewModel vmBatch;

        public WordsUnitBatchEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmBatch = (WordsUnitBatchEditViewModel)BindingContext;
        }

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MUnitWord)((TappedEventArgs)e).Parameter;
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