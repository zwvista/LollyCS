﻿using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class TransformEditViewModel : ReactiveObject, IDragSource
    {
        public string TRANSFORM { get; private set; }
        public string TEMPLATE { get; private set; }
        public bool IsEditing { get; set; }
        public ObservableCollection<MTransformItem> TransformItems { get; }
        public TransformEditViewModel(string transform, string template)
        {
            TRANSFORM = transform;
            TEMPLATE = template;
            var arr = transform.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            TransformItems = new ObservableCollection<MTransformItem>(
                arr.Take(arr.Length / 2 * 2).Buffer(2).Select((g, i) => new MTransformItem { Index = i + 1, Extractor = g[0], Replacement = g[1] })
            );
        }
        public void OnOK()
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
        bool IDragSource.CanStartDrag(IDragInfo dragInfo) => !IsEditing;
        void IDragSource.Dropped(IDropInfo dropInfo) { }
        void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo) =>
            TransformItems.ForEach((o, i) => o.Index = i + 1);
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
