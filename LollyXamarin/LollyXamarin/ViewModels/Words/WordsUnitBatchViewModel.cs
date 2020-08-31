using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class WordsUnitBatchViewModel : ReactiveObject
    {
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        public WordsUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        [Reactive]
        public bool UnitIsChecked { get; set; }
        [Reactive]
        public bool PartIsChecked { get; set; }
        [Reactive]
        public bool SeqNumIsChecked { get; set; }
        [Reactive]
        public bool LevelIsChecked { get; set; }
        [Reactive]
        public bool Level0OnlyIsChecked { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public int LEVEL { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; }

        public WordsUnitBatchViewModel(WordsUnitViewModel vm)
        {
            this.vm = vm;
            foreach (var o in vm.WordItems)
                o.IsChecked = false;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.WordItems)
                {
                    if (UnitIsChecked || PartIsChecked || SeqNumIsChecked)
                    {
                        if (UnitIsChecked) o.UNIT = UNIT;
                        if (PartIsChecked) o.PART = PART;
                        if (SeqNumIsChecked) o.SEQNUM += SEQNUM;
                        await unitWordDS.Update(o);
                    }
                    if (LevelIsChecked && (!Level0OnlyIsChecked || o.LEVEL == 0))
                        await wordFamiDS.Update(o.WORDID, LEVEL);
                }
            });
        }

        public void CheckItems(int n, List<MUnitWord> checkedItems)
        {
            foreach (var o in vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
