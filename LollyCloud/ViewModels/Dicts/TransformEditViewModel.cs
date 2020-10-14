using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using LollyCommon;
using System;
using System.Collections;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace LollyCloud
{
    public class TransformEditViewModelWPF : TransformEditViewModel, IDragSource
    {
        public TransformEditViewModelWPF(SettingsViewModel vmSettings, MDictionaryEdit itemEdit) : base(vmSettings, itemEdit)
        {
        }

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
        void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo) => Reindex();
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
