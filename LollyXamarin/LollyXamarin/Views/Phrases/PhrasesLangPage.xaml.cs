﻿using System;
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
    public partial class PhrasesLangPage : ContentPage
    {
        PhrasesLangViewModel vm;

        public PhrasesLangPage()
        {
            InitializeComponent();
            BindingContext = vm = new PhrasesLangViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            var item = (MLangPhrase)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new PhrasesLangDetailPage
            {
                BindingContext = new PhrasesLangDetailViewModel(vm, item),
            });
        }
    }
}