using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LollyCommon
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
        public bool IsTestMode => Options.Mode == ReviewMode.Test || Options.Mode == ReviewMode.Textbook;
        public MReviewOptions Options { get; set; } = new MReviewOptions();
        public IDisposable SubscriptionTimer;
        public Action DoTestAction;
        public bool IsSpeaking { get; set; }
        [Reactive]
        public string IndexString { get; set; }
        [Reactive]
        public bool IndexIsVisible { get; set; } = true;
        [Reactive]
        public bool CorrectIsVisible { get; set; }
        [Reactive]
        public bool IncorrectIsVisible { get; set; }
        [Reactive]
        public string AccuracyString { get; set; }
        [Reactive]
        public bool AccuracyIsVisible { get; set; } = true;
        [Reactive]
        public bool CheckEnabled { get; set; }
        [Reactive]
        public string WordTargetString { get; set; }
        [Reactive]
        public string NoteTargetString { get; set; }
        [Reactive]
        public string WordHintString { get; set; }
        [Reactive]
        public bool WordHintIsVisible { get; set; }
        [Reactive]
        public bool WordTargetIsVisible { get; set; } = true;
        [Reactive]
        public bool NoteTargetIsVisible { get; set; } = true;
        [Reactive]
        public string TranslationString { get; set; }
        [Reactive]
        public string WordInputString { get; set; }
        [Reactive]
        public string CheckString { get; set; } = "Check";
        [Reactive]
        public bool SearchEnabled { get; set; }
        [Reactive]
        public bool GoogleEnabled { get; set; }

        public WordsReviewViewModel(SettingsViewModel vmSettings, bool needCopy, Action doTestAction)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            DoTestAction = doTestAction;
        }

        public async Task NewTest()
        {
            SubscriptionTimer?.Dispose();
            if (Options.Mode == ReviewMode.Textbook)
            {
                var rand = new Random();
                var lst = await unitWordDS.GetDataByTextbook(vmSettings.SelectedTextbook);
                var lst2 = new List<MUnitWord>();
                foreach (var o in lst)
                {
                    var s = o.ACCURACY;
                    double percentage = !s.EndsWith("%") ? 0 : double.Parse(s.TrimEnd('%'));
                    int t = 6 - (int)(percentage / 20);
                    Enumerable.Range(0, t).ForEach(_ => lst2.Add(o));
                }
                Items = new List<MUnitWord>();
                int cnt = Math.Min(Options.ReviewCount, lst.Count);
                while (Items.Count < cnt)
                {
                    var o = lst2[rand.Next(lst2.Count)];
                    if (!Items.Contains(o))
                        Items.Add(o);
                }
            }
            else
            {
                Items = await unitWordDS.GetDataByTextbookUnitPart(
                    vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
                int nFrom = Count * (Options.GroupSelected - 1) / Options.GroupCount;
                int nTo = Count * Options.GroupSelected / Options.GroupCount;
                Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
                if (Options.Shuffled)
                    Items.Shuffle();
            }
            CorrectIDs = new List<int>();
            Index = 0;
            await DoTest();
            CheckString = IsTestMode ? "Check" : "Next";
            if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval), RxApp.MainThreadScheduler).Subscribe(async _ => await Check());
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
        async Task<string> GetTranslation()
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
                var b = true;
                if (Options.Mode == ReviewMode.ReviewManual && !WordInputString.IsEmpty() && WordInputString != CurrentWord)
                {
                    b = false;
                    IncorrectIsVisible = true;
                }
                if (b)
                {
                    Next();
                    await DoTest();
                }
            }
            else if (!CorrectIsVisible && !IncorrectIsVisible)
            {
                WordInputString = vmSettings.AutoCorrectInput(WordInputString);
                WordTargetIsVisible = true;
                NoteTargetIsVisible = true;
                if (WordInputString == CurrentWord)
                    CorrectIsVisible = true;
                else
                    IncorrectIsVisible = true;
                WordHintIsVisible = false;
                GoogleEnabled = SearchEnabled = true;
                CheckString = "Next";
                if (!HasNext) return;
                var o = CurrentItem;
                var isCorrect = o.WORD == WordInputString;
                if (isCorrect) CorrectIDs.Add(o.ID);
                var o2 = await wordFamiDS.Update(o.WORDID, isCorrect);
                o.CORRECT = o2.CORRECT;
                o.TOTAL = o2.TOTAL;
                AccuracyString = o.ACCURACY;
            }
            else
            {
                Next();
                await DoTest();
                CheckString = "Check";
            }
        }
        async Task DoTest()
        {
            IndexIsVisible = HasNext;
            CorrectIsVisible = false;
            IncorrectIsVisible = false;
            AccuracyIsVisible = IsTestMode && HasNext;
            CheckEnabled = HasNext;
            WordTargetString = CurrentWord;
            NoteTargetString = CurrentItem?.NOTE ?? "";
            WordHintString = CurrentItem?.WORD.Length.ToString() ?? "";
            WordTargetIsVisible = !IsTestMode;
            NoteTargetIsVisible = !IsTestMode;
            WordHintIsVisible = IsTestMode;
            TranslationString = "";
            WordInputString = "";
            GoogleEnabled = SearchEnabled = false;
            DoTestAction?.Invoke();
            if (HasNext)
            {
                IndexString = $"{Index + 1}/{Count}";
                AccuracyString = CurrentItem.ACCURACY;
                TranslationString = await GetTranslation();
                if (string.IsNullOrEmpty(TranslationString) && !Options.SpeakingEnabled)
                    WordInputString = CurrentWord;
            }
            else if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer?.Dispose();
        }
    }
}
