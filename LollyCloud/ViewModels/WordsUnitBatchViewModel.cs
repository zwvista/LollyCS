using ReactiveUI;

namespace LollyShared
{
    public class WordsUnitBatchViewModel : LollyViewModel
    {
        public WordsUnitViewModel vm { get; set; }
        public MTextbook Textbook => vm.vmSettings.SelectedTextbook;

        bool _IsUnitChecked;
        public bool IsUnitChecked
        {
            get => _IsUnitChecked;
            set => this.RaiseAndSetIfChanged(ref _IsUnitChecked, value);
        }
        bool _IsPartChecked;
        public bool IsPartChecked
        {
            get => _IsPartChecked;
            set => this.RaiseAndSetIfChanged(ref _IsPartChecked, value);
        }
        bool _IsSeqNumChecked;
        public bool IsSeqNumChecked
        {
            get => _IsSeqNumChecked;
            set => this.RaiseAndSetIfChanged(ref _IsSeqNumChecked, value);
        }
        bool _IsLevelChecked;
        public bool IsLevelChecked
        {
            get => _IsLevelChecked;
            set => this.RaiseAndSetIfChanged(ref _IsLevelChecked, value);
        }
        bool _IsLevel0OnlyChecked;
        public bool IsLevel0OnlyChecked
        {
            get => _IsLevel0OnlyChecked;
            set => this.RaiseAndSetIfChanged(ref _IsLevel0OnlyChecked, value);
        }
        int _UNIT;
        public int UNIT
        {
            get => _UNIT;
            set => this.RaiseAndSetIfChanged(ref _UNIT, value);
        }
        int _PART;
        public int PART
        {
            get => _PART;
            set => this.RaiseAndSetIfChanged(ref _PART, value);
        }
        int _SEQNUM;
        public int SEQNUM
        {
            get => _SEQNUM;
            set => this.RaiseAndSetIfChanged(ref _SEQNUM, value);
        }
        int _LEVEL;
        public int LEVEL
        {
            get => _LEVEL;
            set => this.RaiseAndSetIfChanged(ref _LEVEL, value);
        }
    }
}
