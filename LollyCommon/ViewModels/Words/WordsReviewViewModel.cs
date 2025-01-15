using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsReviewViewModel : WordsBaseViewModel
    {
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        MDictionary DictTranslation => vmSettings.SelectedDictTranslation;

        public List<MUnitWord> Items { get; set; } = [];
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; } = [];
        [Reactive]
        public int Index { get; set; }
        public bool HasCurrent => Items.Any() && (OnRepeat || (Index >= 0 && Index < Count));
        public MUnitWord CurrentItem => HasCurrent ? Items[Index] : null;
        public string CurrentWord => HasCurrent ? Items[Index].WORD : "";
        public bool IsTestMode => Options.Mode == ReviewMode.Test || Options.Mode == ReviewMode.Textbook;
        public MReviewOptions Options { get; set; } = new MReviewOptions();
        public IDisposable SubscriptionTimer;
        public Action DoTestAction;
        [Reactive]
        public bool IsSpeaking { get; set; }
        [Reactive]
        public string IndexString { get; set; }
        [Reactive]
        public bool IndexVisible { get; set; } = true;
        [Reactive]
        public bool CorrectVisible { get; set; }
        [Reactive]
        public bool IncorrectVisible { get; set; }
        [Reactive]
        public string AccuracyString { get; set; }
        [Reactive]
        public bool AccuracyVisible { get; set; } = true;
        [Reactive]
        public bool CheckNextEnabled { get; set; }
        [Reactive]
        public string CheckNextString { get; set; } = "Check";
        [Reactive]
        public bool CheckPrevEnabled { get; set; }
        [Reactive]
        public string CheckPrevString { get; set; } = "Check";
        [Reactive]
        public bool CheckPrevVisible { get; set; } = true;
        [Reactive]
        public string WordTargetString { get; set; }
        [Reactive]
        public string NoteTargetString { get; set; }
        [Reactive]
        public string WordHintString { get; set; }
        [Reactive]
        public bool WordHintVisible { get; set; }
        [Reactive]
        public bool WordTargetVisible { get; set; } = true;
        [Reactive]
        public bool NoteTargetVisible { get; set; } = true;
        [Reactive]
        public string TranslationString { get; set; }
        [Reactive]
        public string WordInputString { get; set; }
        [Reactive]
        public bool OnRepeat { get; set; } = true;
        [Reactive]
        public bool MoveForward { get; set; } = true;
        [Reactive]
        public bool OnRepeatVisible { get; set; } = true;
        [Reactive]
        public bool MoveForwardVisible { get; set; } = true;
        public override string SelectedWord => CurrentWord;

        public WordsReviewViewModel(SettingsViewModel vmSettings, bool needCopy, Action doTestAction) : base(vmSettings, needCopy)
        {
            DoTestAction = doTestAction;
        }

        public async Task NewTest()
        {
            Index = 0;
            Items = new List<MUnitWord>();
            CorrectIDs = new List<int>();
            SubscriptionTimer?.Dispose();
            IsSpeaking = Options.SpeakingEnabled;
            MoveForward = Options.MoveForward;
            MoveForwardVisible = !IsTestMode;
            OnRepeat = !IsTestMode && Options.OnRepeat;
            OnRepeatVisible = !IsTestMode;
            CheckPrevVisible = !IsTestMode;
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
            Index = MoveForward ? 0 : Count - 1;
            await DoTest();
            CheckNextString = IsTestMode ? "Check" : "Next";
            CheckPrevString = IsTestMode ? "Check" : "Prev";
            if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval), RxApp.MainThreadScheduler).Subscribe(async _ => await Check(true));
        }
        public void Move(bool toNext)
        {
            void CheckOnRepeat()
            {
                if (OnRepeat)
                    Index = (Index + Count) % Count;
            }
            if (MoveForward == toNext)
            {
                Index++;
                CheckOnRepeat();
                if (IsTestMode && !HasCurrent)
                {
                    Index = 0;
                    Items = Items.Where(o => !CorrectIDs.Contains(o.ID)).ToList();
                }
            }
            else
            {
                Index--;
                CheckOnRepeat();
            }
        }
        async Task<string> GetTranslation()
        {
            if (!vmSettings.HasDictTranslation) return "";
            var url = DictTranslation.UrlString(CurrentWord, vmSettings.AutoCorrects);
            var html = await vmSettings.client.GetStringAsync(url);
            return HtmlTransformService.ExtractTextFromHtml(html, DictTranslation.TRANSFORM, "", (text, _) => text);
        }
        public async Task Check(bool toNext)
        {
            if (!IsTestMode)
            {
                var b = true;
                if (Options.Mode == ReviewMode.ReviewManual && !WordInputString.IsEmpty() && WordInputString != CurrentWord)
                {
                    b = false;
                    IncorrectVisible = true;
                }
                if (b)
                {
                    Move(toNext);
                    await DoTest();
                }
            }
            else if (!CorrectVisible && !IncorrectVisible)
            {
                WordInputString = vmSettings.AutoCorrectInput(WordInputString);
                WordTargetVisible = true;
                NoteTargetVisible = true;
                if (WordInputString == CurrentWord)
                    CorrectVisible = true;
                else
                    IncorrectVisible = true;
                WordHintVisible = false;
                CheckNextString = "Next";
                CheckPrevString = "Next";
                if (!HasCurrent) return;
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
                Move(toNext);
                await DoTest();
                CheckNextString = "Check";
                CheckPrevString = "Check";
            }
        }
        async Task DoTest()
        {
            IndexVisible = HasCurrent;
            CorrectVisible = false;
            IncorrectVisible = false;
            AccuracyVisible = IsTestMode && HasCurrent;
            CheckNextEnabled = HasCurrent;
            CheckPrevEnabled = HasCurrent;
            WordTargetString = CurrentWord;
            NoteTargetString = CurrentItem?.NOTE ?? "";
            WordHintString = CurrentItem?.WORD.Length.ToString() ?? "";
            WordTargetVisible = !IsTestMode;
            NoteTargetVisible = !IsTestMode;
            WordHintVisible = IsTestMode;
            TranslationString = "";
            WordInputString = "";
            DoTestAction?.Invoke();
            if (HasCurrent)
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
