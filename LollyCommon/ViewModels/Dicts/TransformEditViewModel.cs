using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCommon
{
    public partial class TransformEditViewModel : ReactiveObject
    {
        [Reactive]
        public partial string TEMPLATE { get; set; }
        public string URL { get; }
        [Reactive]
        public partial string SourceWord { get; set; }
        [Reactive]
        public partial string SourceUrl { get; private set; }
        [Reactive]
        public partial string SourceText { get; set; }
        [Reactive]
        public partial string ResultText { get; private set; }
        [Reactive]
        public partial string ResultHtml { get; private set; }
        [Reactive]
        public partial string IntermediateText { get; private set; }
        [Reactive]
        public partial int IntermediateMaxIndex { get; private set; }
        [Reactive]
        public partial int IntermediateIndex { get; set; }
        public ObservableCollection<MTransformItem> TransformItems { get; }
        [Reactive]
        public partial List<string> IntermediateResults { get; private set; } = [""];
        public ReactiveCommand<Unit, Unit> GetHtmlCommand { get; }
        public ReactiveCommand<Unit, Unit> ExecuteTransformCommand { get; }
        public ReactiveCommand<Unit, Unit> GetAndTransformCommand { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public TransformEditViewModel(SettingsViewModel vmSettings, MDictionaryEdit itemEdit)
        {
            TEMPLATE = itemEdit.TEMPLATE;
            URL = itemEdit.URL;
            TransformItems = new ObservableCollection<MTransformItem>(HtmlTransformService.ToTransformItems(itemEdit.TRANSFORM));
            this.WhenAnyValue(x => x.IntermediateResults).Subscribe(_ => IntermediateText = IntermediateResults[IntermediateIndex = 0]);
            this.WhenAnyValue(x => x.IntermediateIndex).Subscribe(_ => IntermediateText = IntermediateResults[IntermediateIndex]);
            GetHtmlCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                SourceUrl = string.Format(URL, SourceWord);
                SourceText = await vmSettings.client.GetStringAsync(SourceUrl);
            });
            ExecuteTransformCommand = ReactiveCommand.Create(() =>
            {
                var text = HtmlTransformService.RemoveReturns(SourceText);
                IntermediateResults = new List<string> { text };
                foreach (var item in TransformItems)
                {
                    text = HtmlTransformService.DoTransform(text, item);
                    IntermediateResults.Add(text);
                }
                IntermediateMaxIndex = IntermediateResults.Count - 1;
                ResultText = text;
                ResultHtml = string.IsNullOrEmpty(TEMPLATE) ? HtmlTransformService.ToHtml(text) :
                    HtmlTransformService.ApplyTemplate(TEMPLATE, SourceWord, text);
            });
            GetAndTransformCommand = ReactiveCommand.CreateFromObservable(() => GetHtmlCommand.Execute().Concat(ExecuteTransformCommand.Execute()));
            Save = ReactiveCommand.Create(() =>
            {
                itemEdit.TRANSFORM = string.Join("\r\n", TransformItems.SelectMany(o => new[] { o.Extractor, o.Replacement }));
                itemEdit.TEMPLATE = TEMPLATE;
            });
            TransformItems.CollectionChanged += (s, e) => Reindex();
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
    }
}
