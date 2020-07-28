using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MReviewOptions : ReactiveObject
    {
        public bool IsEmbedded { get; set; }
        public bool CanChangeMode => !IsEmbedded;
        [Reactive]
        public ReviewMode Mode { get; set; } = ReviewMode.ReviewAuto;
        [Reactive]
        public bool Shuffled { get; set; } = true;
        [Reactive]
        public bool Levelge0only { get; set; } = true;
        [Reactive]
        public int Interval { get; set; } = 3;
        [Reactive]
        public int GroupSelected { get; set; } = 1;
        [Reactive]
        public int GroupCount { get; set; } = 1;
    }
}
