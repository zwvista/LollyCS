using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class PatternsSplitViewModel : ReactiveValidationObject
    {
        PatternDataStore patternDS = new PatternDataStore();
        public ObservableCollection<MPattern> PatternItems { get; set; }
        // https://stackoverflow.com/questions/1427471/observablecollection-not-noticing-when-item-in-it-changes-even-with-inotifyprop
        public BindingList<MPatternVariation> PatternVariations { get; set; }
        public MPatternEdit SplitItemEdit { get; } = new MPatternEdit();
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PatternsSplitViewModel(MPattern item)
        {
            PatternItems = new ObservableCollection<MPattern>(new[] { item });
            var strs = item.PATTERN.Split('／').ToList();
            PatternVariations = new BindingList<MPatternVariation>(strs.Select((s, i) => new MPatternVariation { Index = i + 1, Variation = s }).ToList());
            Action f = () => SplitItemEdit.PATTERN = string.Join(",", PatternVariations.Select(o => o.Variation));
            PatternVariations.ListChanged += (s, e) =>
            {
                Reindex();
                f();
            };
            f();
            SplitItemEdit.ID = item.ID;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                var splitItem = new MPattern
                {
                    ID = SplitItemEdit.ID,
                    PATTERNS_SPLIT = SplitItemEdit.PATTERN,
                };
                await patternDS.SplitPattern(splitItem);
            }, SplitItemEdit.IsValid());
        }

        public void Reindex() =>
            PatternVariations.ForEach((o, i) => o.Index = i + 1);
    }
}
