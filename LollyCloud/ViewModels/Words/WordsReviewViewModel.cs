using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class WordsReviewViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        MDictionary DictTranslation => vmSettings.SelectedDictTranslation;

        public List<MUnitWord> Items { get; set; }
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; }
        [Reactive]
        public int Index { get; set; }
        public bool HasNext => Index < Count;
        public MUnitWord CurrentItem => HasNext ? Items[Index] : null;
        public string CurrentWord => HasNext ? Items[Index].WORD : "";
        public bool IsTestMode => Options.Mode == ReviewMode.Test;
        public MReviewOptions Options { get; set; } = new MReviewOptions();
        public IDisposable SubscriptionTimer;
        public Action DoTestAction;
        public bool IsSpeaking { get; set; }
        [Reactive]
        public string IndexString { get; set; }
        [Reactive]
        public Visibility IndexVisibility { get; set; }
        [Reactive]
        public Visibility CorrectVisibility { get; set; }
        [Reactive]
        public Visibility IncorrectVisibility { get; set; }
        [Reactive]
        public string AccuracyString { get; set; }
        [Reactive]
        public Visibility AccuracyVisibility { get; set; }
        [Reactive]
        public bool CheckEnabled { get; set; }
        [Reactive]
        public string WordTargetString { get; set; }
        [Reactive]
        public string NoteTargetString { get; set; }
        [Reactive]
        public Visibility WordTargetVisibility { get; set; }
        [Reactive]
        public Visibility NoteTargetVisibility { get; set; }
        [Reactive]
        public string TranslationString { get; set; }
        [Reactive]
        public string WordInputString { get; set; }
        [Reactive]
        public string CheckString { get; set; }

        public WordsReviewViewModel(SettingsViewModel vmSettings, bool needCopy, Action doTestAction)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            DoTestAction = doTestAction;
        }

        public async Task NewTest()
        {
            SubscriptionTimer?.Dispose();
            Items = await unitWordDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
            int nFrom = Count * (Options.GroupSelected - 1) / Options.GroupCount;
            int nTo = Count * Options.GroupSelected / Options.GroupCount;
            Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
            if (Options.Levelge0only)
                Items = Items.Where(o => o.LEVEL >= 0).ToList();
            if (Options.Shuffled)
                Items.Shuffle();
            CorrectIDs = new List<int>();
            Index = 0;
            await DoTest();
            CheckString = IsTestMode ? "Check" : "Next";
            if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval), RxApp.MainThreadScheduler).Subscribe(async _ => await Check());
            else
                SubscriptionTimer?.Dispose();
        }
        public void Next()
        {
            Index++;
            if (IsTestMode && !HasNext)
            {
                Index = 0;
                Items = Items.Where(o => !CorrectIDs.Contains(o.ID)).ToList();
            }
        }
        public async Task<string> GetTranslation()
        {
            if (!vmSettings.HasDictTranslation) return "";
            var url = DictTranslation.UrlString(CurrentWord, vmSettings.AutoCorrects);
            var html = await vmSettings.client.GetStringAsync(url);
            return HtmlTransformService.ExtractTextFromHtml(html, DictTranslation.TRANSFORM, "", (text, _) => text);
        }
        public async Task Check()
        {
            if (!IsTestMode)
            {
                Next();
                await DoTest();
            }
            else if (CorrectVisibility != Visibility.Visible && IncorrectVisibility != Visibility.Visible)
            {
                WordInputString = vmSettings.AutoCorrectInput(WordInputString);
                WordTargetVisibility = Visibility.Visible;
                NoteTargetVisibility = Visibility.Visible;
                if (WordInputString == CurrentWord)
                    CorrectVisibility = Visibility.Visible;
                else
                    IncorrectVisibility = Visibility.Visible;
                CheckString = "Next";
                if (!HasNext) return;
                var o = CurrentItem;
                var isCorrect = o.WORD == WordInputString;
                if (isCorrect) CorrectIDs.Add(o.ID);
                var o2 = await wordFamiDS.Update(o.WORDID, isCorrect);
                o.CORRECT = o2.CORRECT;
                o.TOTAL = o2.TOTAL;
            }
            else
            {
                Next();
                await DoTest();
                CheckString = "Check";
            }
        }
        public async Task DoTest()
        {
            IndexVisibility = HasNext ? Visibility.Visible : Visibility.Hidden;
            CorrectVisibility = Visibility.Hidden;
            IncorrectVisibility = Visibility.Hidden;
            AccuracyVisibility = IsTestMode && HasNext ? Visibility.Visible : Visibility.Hidden;
            CheckEnabled = HasNext;
            WordTargetString = CurrentWord;
            NoteTargetString = CurrentItem?.NOTE ?? "";
            WordTargetVisibility = !IsTestMode ? Visibility.Visible : Visibility.Hidden;
            NoteTargetVisibility = !IsTestMode ? Visibility.Visible : Visibility.Hidden;
            TranslationString = "";
            WordInputString = "";
            DoTestAction?.Invoke();
            if (HasNext)
            {
                IndexString = $"{Index + 1}/{Count}";
                AccuracyString = CurrentItem.ACCURACY;
                TranslationString = await GetTranslation();
            }
            else if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer?.Dispose();
        }
    }
}
