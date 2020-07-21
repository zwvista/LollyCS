using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class PhrasesUnitBatchViewModel : ReactiveObject
    {
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        public PhrasesUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        [Reactive]
        public bool IsUnitChecked { get; set; }
        [Reactive]
        public bool IsPartChecked { get; set; }
        [Reactive]
        public bool IsSeqNumChecked { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }

        public PhrasesUnitBatchViewModel(PhrasesUnitViewModel vm)
        {
            this.vm = vm;
            foreach (var o in vm.PhraseItems)
                o.IsChecked = false;
        }

        public void CheckItems(int n, List<MUnitPhrase> checkedItems)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
        public async Task OnOK()
        {
            foreach (var o in vm.PhraseItems)
                if (IsUnitChecked || IsPartChecked || IsSeqNumChecked)
                {
                    if (IsUnitChecked) o.UNIT = UNIT;
                    if (IsPartChecked) o.PART = PART;
                    if (IsSeqNumChecked) o.SEQNUM += SEQNUM;
                    await unitPhraseDS.Update(o);
                }
        }
    }
}
