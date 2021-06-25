using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace LollyCommon
{
    public enum ReviewMode
    {
        ReviewAuto, ReviewManual, Test, Textbook
    }
    public class MReviewOptions : ReactiveObject
    {
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
        [Reactive]
        public bool SpeakingEnabled { get; set; } = true;
        [Reactive]
        public int ReviewCount { get; set; } = 10;
        [Reactive]
        public bool OnRepeat { get; set; } = true;
        [Reactive]
        public bool MoveForward { get; set; } = true;
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
