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
    public class PhrasesReviewViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();

        public List<MUnitPhrase> Items { get; set; }
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; }
        [Reactive]
        public int Index { get; set; }
        public bool HasNext => Index < Count;
        public MUnitPhrase CurrentItem => HasNext ? Items[Index] : null;
        public string CurrentPhrase => HasNext ? Items[Index].PHRASE : "";
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
        public bool CheckEnabled { get; set; }
        [Reactive]
        public string PhraseTargetString { get; set; }
        [Reactive]
        public Visibility PhraseTargetVisibility { get; set; }
        [Reactive]
        public string TranslationString { get; set; }
        [Reactive]
        public string PhraseInputString { get; set; }
        [Reactive]
        public string CheckString { get; set; }

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public PhrasesReviewViewModel(SettingsViewModel vmSettings, bool needCopy, Action doTestAction)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            DoTestAction = doTestAction;
        }

        public async Task NewTest()
        {
            Items = await unitPhraseDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
            int nFrom = Count * (Options.GroupSelected - 1) / Options.GroupCount;
            int nTo = Count * Options.GroupSelected / Options.GroupCount;
            Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
            if (Options.Shuffled)
                Items.Shuffle();
            CorrectIDs = new List<int>();
            Index = 0;
            DoTest();
            CheckString = IsTestMode ? "Check" : "Next";
            if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval), RxApp.MainThreadScheduler).Subscribe(_ => Check());
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
        public void Check()
        {
            if (!IsTestMode)
            {
                Next();
                DoTest();
            }
            else if (CorrectVisibility != Visibility.Visible && IncorrectVisibility != Visibility.Visible)
            {
                PhraseInputString = vmSettings.AutoCorrectInput(PhraseInputString);
                PhraseTargetVisibility = Visibility.Hidden;
                if (PhraseInputString == CurrentPhrase)
                    CorrectVisibility = Visibility.Visible;
                else
                    IncorrectVisibility = Visibility.Visible;
                CheckString = "Next";
                if (!HasNext) return;
                var o = CurrentItem;
                var isCorrect = o.PHRASE == PhraseInputString;
                if (isCorrect) CorrectIDs.Add(o.ID);
            }
            else
            {
                Next();
                DoTest();
                CheckString = "Check";
            }
        }
        public void DoTest()
        {
            IndexVisibility = HasNext ? Visibility.Visible : Visibility.Hidden;
            CorrectVisibility = Visibility.Hidden;
            IncorrectVisibility = Visibility.Hidden;
            CheckEnabled = HasNext;
            PhraseTargetString = CurrentPhrase;
            TranslationString = CurrentItem?.TRANSLATION ?? "";
            PhraseTargetVisibility = !IsTestMode ? Visibility.Visible : Visibility.Hidden;
            PhraseInputString = "";
            DoTestAction?.Invoke();
            if (HasNext)
                IndexString = $"{Index + 1}/{Count}";
            else if (Options.Mode == ReviewMode.ReviewAuto)
                SubscriptionTimer?.Dispose();
        }
    }
}
