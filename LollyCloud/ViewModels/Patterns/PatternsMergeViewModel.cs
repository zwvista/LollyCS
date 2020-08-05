using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class PatternsMergeViewModel : ReactiveValidationObject<PatternsMergeViewModel>
    {
        public ObservableCollection<MPattern> PatternItems { get; set; }
        // https://stackoverflow.com/questions/1427471/observablecollection-not-noticing-when-item-in-it-changes-even-with-inotifyprop
        public BindingList<MPattern> PatternVariations { get; set; }
        public MPatternEdit MergedItemEdit { get; } = new MPatternEdit();
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PatternsMergeViewModel(List<MPattern> items)
        {
            PatternItems = new ObservableCollection<MPattern>(items);
            var strs = items.SelectMany(o => o.PATTERN.Split('／')).OrderBy(s => s).Distinct().ToList();
            PatternVariations = new BindingList<MPattern>(strs.Select(s => new MPattern { PATTERN = s }).ToList());
            Action f = () => MergedItemEdit.PATTERN = string.Join("／", PatternVariations.Select(o => o.PATTERN));
            PatternVariations.ListChanged += (s, e) => f();
            f();
            MergedItemEdit.NOTE = items.Select(o => o.NOTE).SplitByCommaAndMerge();
            MergedItemEdit.TAGS = items.Select(o => o.TAGS).SplitByCommaAndMerge();
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                var mergedItem = new MPattern();
                MergedItemEdit.CopyProperties(mergedItem);
                await MergePatterns(PatternItems.ToList(), mergedItem);
            }, MergedItemEdit.IsValid());
        }

        public static async Task MergePatterns(List<MPattern> sourceItems, MPattern mergedItem)
        {
            var storept = new PatternDataStore();
            var storeptwp = new PatternWebPageDataStore();

            var lstpt = sourceItems.OrderBy(o => o.ID).ToList();
            var lstptwp = (await lstpt.ToObservable().SelectMany(o => storeptwp.GetDataByPattern(o.ID).ToObservable()).ToList().ToTask()).SelectMany(o => o).ToList();

            var o1 = lstpt[0];
            var ptid = o1.ID;
            mergedItem.CopyProperties(o1, nameof(MPattern.ID));
            await storept.Update(o1);

            lstpt.Skip(1).ForEach(async o => await storept.Delete(o.ID));

            lstptwp.ForEach(async (o, i) =>
            {
                o.PATTERNID = ptid;
                o.SEQNUM = i + 1;
                await storeptwp.Update(o);
            });
        }
        public static async Task AutoMergePatterns()
        {
            var storept = new PatternDataStore();
            var lst = await storept.GetDataByLang(2);
            var dic = lst.GroupBy(o => o.PATTERN).Where(g => g.Count() > 1).ToDictionary(g => g.Key, g => g.ToList());
            foreach (var kv in dic)
            {
                var sourceItems = kv.Value;
                var mergedItem = new MPattern
                {
                    PATTERN = kv.Key,
                    NOTE = sourceItems.Select(o => o.NOTE).SplitByCommaAndMerge(),
                    TAGS = sourceItems.Select(o => o.TAGS).SplitByCommaAndMerge()
                };
                await MergePatterns(sourceItems, mergedItem);
            }
        }
    }
}
