using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace LollyShared
{
    public class PhrasesReviewViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();

        public List<MUnitPhrase> Items { get; set; }
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; }
        int _Index;
        public int Index
        {
            get => _Index;
            set => this.RaiseAndSetIfChanged(ref _Index, value);
        }
        public bool HasNext => Index < Count;
        public MUnitPhrase CurrentItem => HasNext ? Items[Index] : null;
        public string CurrentPhrase => HasNext ? Items[Index].PHRASE : "";
        public bool IsTestMode => Options.Mode == ReviewMode.Test;
        public MReviewOptions Options { get; set; } = new MReviewOptions();

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public PhrasesReviewViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }

        public async void NewTest()
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
        }
        public void Next()
        {
            Index++;
            if (IsTestMode && !HasNext)
            {
                Index = 0;
                Items = Items.Where(o => CorrectIDs.Contains(o.ID)).ToList();
            }
        }
        public void Check(string phraseInput)
        {
            if (!HasNext) return;
            var o = CurrentItem;
            var isCorrect = o.PHRASE == phraseInput;
            if (isCorrect) CorrectIDs.Add(o.ID);
        }
    }
}
