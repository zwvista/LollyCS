﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class PhrasesUnitDetailPage : ContentPage
    {
        PhrasesUnitDetailViewModel vmDetail = null!;

        public PhrasesUnitDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmDetail = (PhrasesUnitDetailViewModel)BindingContext;
            BindingContext = vmDetail.ItemEdit;
            vmDetail.ItemEdit.Save.WhenFinishedExecuting().Subscribe(_ => Navigation.PopModalAsync());
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();
    }
}