﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LollyCommon;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace LollyXamarin
{
    public partial class AppShell : Shell
    {
        public static SettingsViewModel vmSettings = new SettingsViewModel();
        public static List<Locale> SpeechLocales;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(WordsUnitDetailPage), typeof(WordsUnitDetailPage));
            Routing.RegisterRoute(nameof(WordsUnitBatchEditPage), typeof(WordsUnitBatchEditPage));
            Routing.RegisterRoute(nameof(WordsTextbookDetailPage), typeof(WordsTextbookDetailPage));
            Routing.RegisterRoute(nameof(WordsLangDetailPage), typeof(WordsLangDetailPage));
            Routing.RegisterRoute(nameof(WordsDictPage), typeof(WordsDictPage));
            Routing.RegisterRoute(nameof(PhrasesUnitDetailPage), typeof(PhrasesUnitDetailPage));
            Routing.RegisterRoute(nameof(PhrasesUnitBatchEditPage), typeof(PhrasesUnitBatchEditPage));
            Routing.RegisterRoute(nameof(PhrasesTextbookDetailPage), typeof(PhrasesTextbookDetailPage));
            Routing.RegisterRoute(nameof(PhrasesLangDetailPage), typeof(PhrasesLangDetailPage));
            Routing.RegisterRoute(nameof(PatternsDetailPage), typeof(PatternsDetailPage));
            Routing.RegisterRoute(nameof(PatternsWebPagesListPage), typeof(PatternsWebPagesListPage));
            Routing.RegisterRoute(nameof(PatternsWebPagesDetailPage), typeof(PatternsWebPagesDetailPage));
            Routing.RegisterRoute(nameof(PatternsWebPagesBrowsePage), typeof(PatternsWebPagesBrowsePage));
            Routing.RegisterRoute(nameof(ReviewOptionsPage), typeof(ReviewOptionsPage));

            Task.Run(async () =>
            {
                SpeechLocales = (await TextToSpeech.GetLocalesAsync()).ToList();
            });
        }

        public async void OnMenuItemClicked(object sender, EventArgs e)
        {
            CommonApi.UserId = "";
            XamarinCommon.SaveUserId();
            await Current.GoToAsync("//LoginPage");
        }
    }
}
