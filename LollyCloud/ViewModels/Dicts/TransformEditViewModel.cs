using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class TransformEditViewModel : ReactiveObject, IDragSource
    {
        public string TRANSFORM { get; private set; }
        [Reactive]
        public string TEMPLATE { get; set; }
        public string URL { get; }
        public bool IsEditing { get; set; }
        [Reactive]
        public string SourceWord { get; set; }
        [Reactive]
        public string SourceUrl { get; set; }
        [Reactive]
        public string SourceText { get; set; }
        [Reactive]
        public string ResultText { get; private set; }
        [Reactive]
        public string InterimText { get; private set; }
        [Reactive]
        public int InterimMaxIndex { get; private set; }
        [Reactive]
        public int InterimIndex { get; set; }
        public ObservableCollection<MTransformItem> TransformItems { get; }
        [Reactive]
        public List<string> InterimResults { get; private set; } = new List<string> { "" };
        public ReactiveCommand<Unit, Unit> GetHtmlCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> ExecuteTransformCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public TransformEditViewModel(string transform, string template, string url)
        {
            TRANSFORM = transform;
            TEMPLATE = template;
            URL = url;
            TransformItems = new ObservableCollection<MTransformItem>(HtmlTransformService.ToTransformItems(transform));
            this.WhenAnyValue(x => x.InterimResults).Subscribe(_ => InterimText = InterimResults[InterimIndex = 0]);
            this.WhenAnyValue(x => x.InterimIndex).Subscribe(_ => InterimText = InterimResults[InterimIndex]);
            GetHtmlCommand = ReactiveCommand.CreateFromTask(async () => {
                SourceUrl = string.Format(URL, SourceWord);
                SourceText = await MainWindow.vmSettings.client.GetStringAsync(SourceUrl);
            });
            ExecuteTransformCommand = ReactiveCommand.Create(() =>
            {
                var text = HtmlTransformService.RemoveReturns(SourceText);
                InterimResults = new List<string> { text };
                foreach (var item in TransformItems)
                {
                    text = HtmlTransformService.DoTransform(text, item);
                    InterimResults.Add(text);
                }
                InterimMaxIndex = InterimResults.Count - 1;
                ResultText = text;
            });
            Save = ReactiveCommand.Create(() =>
            {
                TRANSFORM = string.Join("\r\n", TransformItems.SelectMany(o => new[] { o.Extractor, o.Replacement }));
            });
        }

        public void Reindex() =>
            TransformItems.ForEach((o, i) => o.Index = i + 1);

        public MTransformItem NewTransformItem() =>
            new MTransformItem
            {
                Index = TransformItems.Count + 1
            };

        public void Add(MTransformItem item)
        {
            TransformItems.Add(item);
            Reindex();
        }

        public void Delete(MTransformItem item)
        {
            TransformItems.Remove(item);
            Reindex();
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
        void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo) => Reindex();
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
