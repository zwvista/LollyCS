using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class WordsUnitBatchViewModel : ReactiveObject
    {
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
    }
}
