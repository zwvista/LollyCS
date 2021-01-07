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
    public partial class WordsLangPage : ContentPage
    {
        WordsLangViewModel vm;

        public WordsLangPage()
        {
            InitializeComponent();
            BindingContext = vm = new WordsLangViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangWord)((TappedEventArgs)e).Parameter;
            Navigation.PushAsync(new WordsLangDetailPage
            {
                BindingContext = new WordsLangDetailViewModel(vm, item),
            });
        }

        void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
        }

        void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
        }

        void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
        }
    }
}