using ReactiveUI;

namespace LollyShared
{
    public class MReviewOptions : ReactiveObject
    {
        ReviewMode _Mode;
        public ReviewMode Mode
        {
            get => _Mode;
            set => this.RaiseAndSetIfChanged(ref _Mode, value);
        }
        bool _Shuffled;
        public bool Shuffled
        {
            get => _Shuffled;
            set => this.RaiseAndSetIfChanged(ref _Shuffled, value);
        }
        bool _Levelge0only;
        public bool Levelge0only
        {
            get => _Levelge0only;
            set => this.RaiseAndSetIfChanged(ref _Levelge0only, value);
        }
        int _Interval;
        public int Interval
        {
            get => _Interval;
            set => this.RaiseAndSetIfChanged(ref _Interval, value);
        }
        int _GroupSelected;
        public int GroupSelected
        {
            get => _GroupSelected;
            set => this.RaiseAndSetIfChanged(ref _GroupSelected, value);
        }
        int _GroupCount;
        public int GroupCount
        {
            get => _GroupCount;
            set => this.RaiseAndSetIfChanged(ref _GroupCount, value);
        }
    }
}
