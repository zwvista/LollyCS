using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace LollyCommon
{
    public enum ReviewMode
    {
        ReviewAuto, ReviewManual, Test, Textbook
    }
    public partial class MReviewOptions : ReactiveObject
    {
        [Reactive]
        public partial ReviewMode Mode { get; set; } = ReviewMode.ReviewAuto;
        [Reactive]
        public partial bool Shuffled { get; set; } = true;
        [Reactive]
        public partial int Interval { get; set; } = 5;
        [Reactive]
        public partial int GroupSelected { get; set; } = 1;
        [Reactive]
        public partial int GroupCount { get; set; } = 1;
        [Reactive]
        public partial bool SpeakingEnabled { get; set; } = true;
        [Reactive]
        public partial int ReviewCount { get; set; } = 10;
        [Reactive]
        public partial bool OnRepeat { get; set; } = true;
        [Reactive]
        public partial bool MoveForward { get; set; } = true;
        public MSelectItem ModeItem
        {
            get => SettingsViewModel.ReviewModes.SingleOrDefault(o => o.Value == (int)Mode);
            set { if (value != null) Mode = (ReviewMode)Enum.ToObject(typeof(ReviewMode), value.Value); }
        }

        public MReviewOptions()
        {
            this.WhenAnyValue(x => x.GroupSelected).Where(v => v > GroupCount).Subscribe(v => GroupCount = v);
            this.WhenAnyValue(x => x.GroupCount).Where(v => v < GroupSelected).Subscribe(v => GroupSelected = v);
        }
    }
}
