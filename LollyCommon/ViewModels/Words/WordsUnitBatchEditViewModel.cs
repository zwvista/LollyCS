using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class WordsUnitBatchEditViewModel : ReactiveObject
    {
        public WordsUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        [Reactive]
        public bool UnitChecked { get; set; }
        [Reactive]
        public bool PartChecked { get; set; }
        [Reactive]
        public bool SeqNumChecked { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        public MSelectItem UNITItem
        {
            get => Textbook.Units.SingleOrDefault(o => o.Value == UNIT);
            set { if (value != null) UNIT = value.Value; }
        }
        public MSelectItem PARTItem
        {
            get => Textbook.Parts.SingleOrDefault(o => o.Value == PART);
            set { if (value != null) PART = value.Value; }
        }
        public ReactiveCommand<Unit, Unit> Save { get; }

        public WordsUnitBatchEditViewModel(WordsUnitViewModel vm)
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
                    if (UnitChecked)
                    {
                        o.UNIT = UNIT;
                        b = true;
                    }
                    if (PartChecked)
                    {
                        o.PART = PART;
                        b = true;
                    }
                    if (SeqNumChecked)
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
