using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public partial class PhrasesUnitBatchEditViewModel : ReactiveObject
    {
        public PhrasesUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        [Reactive]
        public partial bool UnitChecked { get; set; }
        [Reactive]
        public partial bool PartChecked { get; set; }
        [Reactive]
        public partial bool SeqNumChecked { get; set; }
        [Reactive]
        public partial int UNIT { get; set; }
        [Reactive]
        public partial int PART { get; set; }
        [Reactive]
        public partial int SEQNUM { get; set; }
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

        public PhrasesUnitBatchEditViewModel(PhrasesUnitViewModel vm)
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

        public void CheckItems(int n, List<MUnitPhrase> selectedItems)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
