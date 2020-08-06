using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Windows;

namespace LollyCloud
{
    public class PatternsSplitViewModel : ReactiveValidationObject<PatternsSplitViewModel>, IDragSource
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
