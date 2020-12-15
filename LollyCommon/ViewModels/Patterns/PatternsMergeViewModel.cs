using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PatternsMergeViewModel : ReactiveValidationObject
    {
        PatternDataStore patternDS = new PatternDataStore();
        public ObservableCollection<MPattern> PatternItems { get; set; }
        // https://stackoverflow.com/questions/1427471/observablecollection-not-noticing-when-item-in-it-changes-even-with-inotifyprop
        public BindingList<MPatternVariation> PatternVariations { get; set; }
        public MPatternEdit MergedItemEdit { get; } = new MPatternEdit();
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PatternsMergeViewModel(List<MPattern> items)
        {
            PatternItems = new ObservableCollection<MPattern>(items);
            var strs = items.SelectMany(o => o.PATTERN.Split('／')).OrderBy(s => s).Distinct().ToList();
            PatternVariations = new BindingList<MPatternVariation>(strs.Select((s, i) => new MPatternVariation { Index = i + 1, Variation = s }).ToList());
            Action f = () => MergedItemEdit.PATTERN = string.Join("／", PatternVariations.Select(o => o.Variation).Distinct());
            PatternVariations.ListChanged += (s, e) =>
            {
                Reindex();
                f();
            };
            f();
            MergedItemEdit.NOTE = items.Select(o => o.NOTE).SplitUsingCommaAndMerge();
            MergedItemEdit.TAGS = items.Select(o => o.TAGS).SplitUsingCommaAndMerge();
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                var mergedItem = new MPattern
                {
                    IDS_MERGE = string.Join(",", PatternItems.OrderBy(o => o.ID).Select(o => o.ID.ToString())),
                    PATTERN = MergedItemEdit.PATTERN,
                    NOTE = MergedItemEdit.NOTE,
                    TAGS = MergedItemEdit.TAGS,
                };
                await patternDS.MergePatterns(mergedItem);
            }, MergedItemEdit.IsValid());
        }

        public static async Task AutoMergePatterns()
        {
            var storept = new PatternDataStore();
            var lst = await storept.GetDataByLang(2);
            var dic = lst.GroupBy(o => o.PATTERN).Where(g => g.Count() > 1).ToDictionary(g => g.Key, g => g.OrderBy(o => o.ID).ToList());
            foreach (var kv in dic)
            {
                var sourceItems = kv.Value;
                var mergedItem = new MPattern
                {
                    IDS_MERGE = string.Join(",", sourceItems.Select(o => o.ID.ToString())),
                    PATTERN = kv.Key,
                    NOTE = sourceItems.Select(o => o.NOTE).SplitUsingCommaAndMerge(),
                    TAGS = sourceItems.Select(o => o.TAGS).SplitUsingCommaAndMerge()
                };
                await storept.MergePatterns(mergedItem);
            }
        }

        public void Reindex() =>
            PatternVariations.ForEach((o, i) => o.Index = i + 1);
    }
}
