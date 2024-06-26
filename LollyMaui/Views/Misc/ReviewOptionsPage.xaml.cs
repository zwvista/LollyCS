﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class ReviewOptionsPage : ContentPage, IPageOK
    {
        MReviewOptions options = null!;
        MReviewOptions optionsEdit = new MReviewOptions();
        public event EventHandler? OnOK;

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
            OnOK?.Invoke(this, EventArgs.Empty);
        }

        void OnCancel(object sender, EventArgs e) =>
            Navigation.PopModalAsync();
    }
}