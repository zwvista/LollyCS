using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCommon
{
    public class WordsUnitBatchViewModel : ReactiveObject
    {
        public WordsUnitViewModel vm { get; set; }
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

        public WordsUnitBatchViewModel(WordsUnitViewModel vm)
        {
            this.vm = vm;
            foreach (var o in vm.WordItems)
                o.IsChecked = false;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.WordItems)
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

        public void CheckItems(int n, List<MUnitWord> selectedItems)
        {
            foreach (var o in vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
