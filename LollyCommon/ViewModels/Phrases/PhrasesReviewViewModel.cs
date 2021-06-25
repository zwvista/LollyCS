using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PhrasesReviewViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();

        public List<MUnitPhrase> Items { get; set; }
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; }
        [Reactive]
        public int Index { get; set; }
        public bool HasCurrent => Items.Any() && (OnRepeat || (Index >= 0 && Index < Count));
        public MUnitPhrase CurrentItem => HasCurrent ? Items[Index] : null;
        public string CurrentPhrase => HasCurrent ? Items[Index].PHRASE : "";
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
        public string PhraseTargetString { get; set; }
        [Reactive]
        public bool PhraseTargetVisible { get; set; } = true;
        [Reactive]
        public string TranslationString { get; set; }
        [Reactive]
        public string PhraseInputString { get; set; }
        [Reactive]
        public bool OnRepeat { get; set; } = true;
        [Reactive]
        public bool MoveForward { get; set; } = true;
        [Reactive]
        public bool OnRepeatVisible { get; set; } = true;
        [Reactive]
        public bool MoveForwardVisible { get; set; } = true;

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public PhrasesReviewViewModel(SettingsViewModel vmSettings, bool needCopy, Action doTestAction)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            DoTestAction = doTestAction;
        }

        public async Task NewTest()
        {
            Index = 0;
            Items = new List<MUnitPhrase>();
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
                var lst = await unitPhraseDS.GetDataByTextbook(vmSettings.SelectedTextbook);
                int cnt = Math.Min(Options.ReviewCount, lst.Count);
                lst.Shuffle();
                Items = lst.Take(cnt).ToList();
            }
            else
            {
                Items = await unitPhraseDS.GetDataByTextbookUnitPart(
                    vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
                int nFrom = Count * (Options.GroupSelected - 1) / Options.GroupCount;
                int nTo = Count * Options.GroupSelected / Options.GroupCount;
                Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
                if (Options.Shuffled)
                    Items.Shuffle();
            }
            Index = MoveForward ? 0 : Count - 1;
            DoTest();
            CheckNextString = IsTestMode ? "Check" : "Next";
            CheckPrevString = IsTestMode ? "Check" : "Prev";
            if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval), RxApp.MainThreadScheduler).Subscribe(_ => Check(true));
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
        public void Check(bool toNext)
        {
            if (!IsTestMode)
            {
                var b = true;
                if (Options.Mode == ReviewMode.ReviewManual && !PhraseInputString.IsEmpty() && PhraseInputString != CurrentPhrase)
                {
                    b = false;
                    IncorrectVisible = true;
                }
                if (b)
                {
                    Move(toNext);
                    DoTest();
                }
            }
            else if (!CorrectVisible && !IncorrectVisible)
            {
                PhraseInputString = vmSettings.AutoCorrectInput(PhraseInputString);
                PhraseTargetVisible = true;
                if (PhraseInputString == CurrentPhrase)
                    CorrectVisible = true;
                else
                    IncorrectVisible = true;
                CheckNextString = "Next";
                CheckPrevString = "Next";
                if (!HasCurrent) return;
                var o = CurrentItem;
                var isCorrect = o.PHRASE == PhraseInputString;
                if (isCorrect) CorrectIDs.Add(o.ID);
            }
            else
            {
                Move(toNext);
                DoTest();
                CheckNextString = "Check";
                CheckPrevString = "Check";
            }
        }
        public void DoTest()
        {
            IndexVisible = HasCurrent;
            CorrectVisible = false;
            IncorrectVisible = false;
            CheckNextEnabled = HasCurrent;
            CheckPrevEnabled = HasCurrent;
            PhraseTargetString = CurrentPhrase;
            TranslationString = CurrentItem?.TRANSLATION ?? "";
            PhraseTargetVisible = !IsTestMode;
            PhraseInputString = "";
            DoTestAction?.Invoke();
            if (HasCurrent)
                IndexString = $"{Index + 1}/{Count}";
            else if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer?.Dispose();
        }
    }
}
