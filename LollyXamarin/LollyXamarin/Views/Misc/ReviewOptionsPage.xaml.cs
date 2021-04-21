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
    public partial class ReviewOptionsPage : ContentPage
    {
        MReviewOptions options;
        MReviewOptions optionsEdit = new MReviewOptions();

        public ReviewOptionsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            options = (MReviewOptions)BindingContext;
            options.CopyProperties(optionsEdit);
            BindingContext = optionsEdit;
        }

        void OnSave(object sender, EventArgs e)
        {
            optionsEdit.CopyProperties(options);
            Navigation.PopModalAsync();
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();
    }
}