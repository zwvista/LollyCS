using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class WordsUnitBatchViewModel : ReactiveObject
    {
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        public WordsUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        [Reactive]
        public bool IsUnitChecked { get; set; }
        [Reactive]
        public bool IsPartChecked { get; set; }
        [Reactive]
        public bool IsSeqNumChecked { get; set; }
        [Reactive]
        public bool IsLevelChecked { get; set; }
        [Reactive]
        public bool IsLevel0OnlyChecked { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public int LEVEL { get; set; }

        public WordsUnitBatchViewModel(WordsUnitViewModel vm)
        {
            this.vm = vm;
            foreach (var o in vm.WordItems)
                o.IsChecked = false;
        }

        public void CheckItems(int n, List<MUnitWord> checkedItems)
        {
            foreach (var o in vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
        public async Task OnOK()
        {
            foreach (var o in vm.WordItems)
            {
                if (IsUnitChecked || IsPartChecked || IsSeqNumChecked)
                {
                    if (IsUnitChecked) o.UNIT = UNIT;
                    if (IsPartChecked) o.PART = PART;
                    if (IsSeqNumChecked) o.SEQNUM += SEQNUM;
                    await unitWordDS.Update(o);
                }
                if (IsLevelChecked && (!IsLevel0OnlyChecked || o.LEVEL == 0))
                    await wordFamiDS.Update(o.WORDID, LEVEL);
            }
        }
    }
}
