﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;
using ReactiveUI;
using System.Reactive.Linq;

namespace LollyMaui
{
    public partial class PatternsWebPagePage : ContentPage, IPageNavigate
    {
        PatternsDetailViewModel vmDetail = null!;

        public PatternsWebPagePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            vmDetail = (PatternsDetailViewModel)navigationData;
            BindingContext = vmDetail.ItemEdit;
            wbWebPage.Source = vmDetail.ItemEdit.URL;
        }
    }
}