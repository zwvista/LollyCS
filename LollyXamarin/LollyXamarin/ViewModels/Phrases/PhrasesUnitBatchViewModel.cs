using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class PhrasesUnitBatchViewModel : ReactiveObject
    {
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        public PhrasesUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        [Reactive]
        public bool UnitIsChecked { get; set; }
        [Reactive]
        public bool PartIsChecked { get; set; }
        [Reactive]
        public bool SeqNumIsChecked { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PhrasesUnitBatchViewModel(PhrasesUnitViewModel vm)
        {
            this.vm = vm;
            foreach (var o in vm.PhraseItems)
                o.IsChecked = false;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.PhraseItems)
                    if (UnitIsChecked || PartIsChecked || SeqNumIsChecked)
                    {
                        if (UnitIsChecked) o.UNIT = UNIT;
                        if (PartIsChecked) o.PART = PART;
                        if (SeqNumIsChecked) o.SEQNUM += SEQNUM;
                        await unitPhraseDS.Update(o);
                    }
            });
        }

        public void CheckItems(int n, List<MUnitPhrase> checkedItems)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
