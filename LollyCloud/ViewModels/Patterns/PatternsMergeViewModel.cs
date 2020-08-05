using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
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
using System.Windows;

namespace LollyCloud
{
    public class PatternsMergeViewModel : ReactiveValidationObject<PatternsMergeViewModel>, IDragSource
    {
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
            Action f = () => MergedItemEdit.PATTERN = string.Join("／", PatternVariations.Select(o => o.Variation));
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
            // var lstptwp = (await ObservableEx.ForkJoin(lstpt.Select(o => storeptwp.GetDataByPattern(o.ID).ToObservable())).ToTask()).SelectMany(o => o).ToList();
            var lstptwp = (await Task.WhenAll(lstpt.Select(async o => await storeptwp.GetDataByPattern(o.ID)))).SelectMany(o => o).ToList();

            var o1 = lstpt[0];
            var ptid = o1.ID;
            mergedItem.CopyProperties(o1, nameof(MPattern.ID));
            await storept.Update(o1);

            Task.WaitAll(lstpt.Skip(1).Select(async o => await storept.Delete(o.ID)).ToArray());

            Task.WaitAll(lstptwp.Select(async (o, i) =>
            {
                o.PATTERNID = ptid;
                o.SEQNUM = i + 1;
                await storeptwp.Update(o);
            }).ToArray());
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
                    NOTE = sourceItems.Select(o => o.NOTE).SplitUsingCommaAndMerge(),
                    TAGS = sourceItems.Select(o => o.TAGS).SplitUsingCommaAndMerge()
                };
                await MergePatterns(sourceItems, mergedItem);
            }
        }

        public void Reindex() =>
            PatternVariations.ForEach((o, i) => o.Index = i + 1);

        // Copied from DefaultDragHandler
        // https://github.com/punker76/gong-wpf-dragdrop/blob/dev/src/GongSolutions.WPF.DragDrop/DefaultDragHandler.cs
        void IDragSource.StartDrag(IDragInfo dragInfo)
        {
            var items = TypeUtilities.CreateDynamicallyTypedList(dragInfo.SourceItems).Cast<object>().ToList();
            if (items.Count > 1)
            {
                dragInfo.Data = items;
            }
            else
            {
                // special case: if the single item is an enumerable then we can not drop it as single item
                var singleItem = items.FirstOrDefault();
                if (singleItem is IEnumerable && !(singleItem is string))
                {
                    dragInfo.Data = items;
                }
                else
                {
                    dragInfo.Data = singleItem;
                }
            }

            dragInfo.Effects = dragInfo.Data != null ? DragDropEffects.Copy | DragDropEffects.Move : DragDropEffects.None;
        }
        bool IDragSource.CanStartDrag(IDragInfo dragInfo) => true;
        void IDragSource.Dropped(IDropInfo dropInfo) { }
        void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            PatternVariations.ResetBindings();
        }
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
