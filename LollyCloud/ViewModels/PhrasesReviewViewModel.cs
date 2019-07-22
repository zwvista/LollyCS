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
        ReviewMode _Mode = ReviewMode.ReviewAuto;
        public ReviewMode Mode
        {
            get => _Mode;
            set => this.RaiseAndSetIfChanged(ref _Mode, value);
        }
        public bool IsTestMode => Mode == ReviewMode.Test;

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public PhrasesReviewViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }

        public async void NewTest(bool shuffled, int groupSelected, int groupCount)
        {
            Items = await unitPhraseDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
            int nFrom = Count * (groupSelected - 1) / groupCount;
            int nTo = Count * groupSelected / groupCount;
            Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
            if (shuffled)
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
