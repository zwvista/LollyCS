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

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangPhrase)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new PhrasesLangDetailPage
            {
                BindingContext = new PhrasesLangDetailViewModel(vm, item),
            });
        }

        void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var a = await DisplayActionSheet("More", "Cancel", null, "Delete", "Edit", "Copy Phrase", "Google Phrase");
            switch (a)
            {
                case "Delete":
                    break;
                case "Edit":
                    break;
                case "Copy Phrase":
                    break;
                case "Google Phrase":
                    break;
            }
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }
    }
}