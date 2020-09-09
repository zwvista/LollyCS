using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;

namespace LollyCloud
{
    public enum ReviewMode
    {
        ReviewAuto, Test, ReviewManual
    }
    public class MReviewOptions : ReactiveObject
    {
        public bool IsEmbedded { get; set; }
        public bool CanChangeMode => !IsEmbedded;
        [Reactive]
        public ReviewMode Mode { get; set; } = ReviewMode.ReviewAuto;
        [Reactive]
        public bool Shuffled { get; set; } = true;
        [Reactive]
        public int Interval { get; set; } = 5;
        [Reactive]
        public int GroupSelected { get; set; } = 1;
        [Reactive]
        public int GroupCount { get; set; } = 1;

        public MReviewOptions()
        {
            this.WhenAnyValue(x => x.GroupSelected).Where(v => v > GroupCount).Subscribe(v => GroupCount = v);
            this.WhenAnyValue(x => x.GroupCount).Where(v => v < GroupSelected).Subscribe(v => GroupSelected = v);
        }
    }
}
