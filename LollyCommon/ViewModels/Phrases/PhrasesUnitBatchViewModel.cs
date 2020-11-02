using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCommon
{
    public class PhrasesUnitBatchViewModel : ReactiveObject
    {
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
                {
                    if (!o.IsChecked) continue;
                    bool b = false;
                    if (UnitIsChecked)
                    {
                        o.UNIT = UNIT;
                        b = true;
                    }
                    if (PartIsChecked)
                    {
                        o.PART = PART;
                        b = true;
                    }
                    if (SeqNumIsChecked)
                    {
                        o.SEQNUM += SEQNUM;
                        b = true;
                    }
                    if (b)
                        await vm.Update(o);
                }
            });
        }

        public void CheckItems(int n, List<MUnitPhrase> selectedItems)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
