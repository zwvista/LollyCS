using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class PatternsViewModel : ReactiveObject, IDragSource
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();
        PatternWebPageDataStore patternWebPageDS = new PatternWebPageDataStore();
        WebPageDataStore webPageDS = new WebPageDataStore();
        PatternPhraseDataStore patternPhraseDS = new PatternPhraseDataStore();

        public ObservableCollection<MPattern> PatternItemsAll { get; set; }
        public ObservableCollection<MPattern> PatternItemsFiltered { get; set; }
        public ObservableCollection<MPattern> PatternItems => PatternItemsFiltered ?? PatternItemsAll;
        public ObservableCollection<MPatternWebPage> WebPageItems { get; set; }
        public ObservableCollection<MPatternPhrase> PhraseItems { get; set; }
        [Reactive]
        public string TextFilter { get; set; }
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePatternFilters[0];
        public bool IsEditing { get; set; }

        public PatternsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ =>
            {
                PatternItemsFiltered = string.IsNullOrEmpty(TextFilter) ? null :
                new ObservableCollection<MPattern>(PatternItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Pattern" ? o.PATTERN : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower()))
                ));
                this.RaisePropertyChanged(nameof(PatternItems));
            });
            Reload();
        }
        public void Reload() =>
            patternDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PatternItemsAll = new ObservableCollection<MPattern>(lst);
                this.RaisePropertyChanged(nameof(PatternItems));
            });

        public async Task Update(MPattern item) => await patternDS.Update(item);
        public async Task<int> Create(MPattern item) => await patternDS.Create(item);
        public async Task Delete(int id) => await patternDS.Delete(id);

        public MPattern NewPattern() =>
            new MPattern
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

        public async Task GetWebPages(int patternid)
        {
            WebPageItems = new ObservableCollection<MPatternWebPage>(await patternWebPageDS.GetDataByPattern(patternid));
            this.RaisePropertyChanged(nameof(WebPageItems));
        }
        public async Task UpdatePatternWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Update(item);
        public async Task<int> CreatePatternWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Create(item);
        public async Task DeletePatternWebPage(int id) =>
            await patternWebPageDS.Delete(id);
        public async Task UpdateWebPage(MPatternWebPage item) =>
            await webPageDS.Update(item);
        public async Task<int> CreateWebPage(MPatternWebPage item) =>
            await webPageDS.Create(item);
        public async Task DeleteWebPage(int id) =>
            await webPageDS.Delete(id);
        public MPatternWebPage NewPatternWebPage(int patternid, string pattern) =>
            new MPatternWebPage
            {
                PATTERNID = patternid,
                PATTERN = pattern,
                SEQNUM = WebPageItems.Select(o => o.SEQNUM).StartWith(0).Max() + 1
            };

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= WebPageItems.Count; i++)
            {
                var item = WebPageItems[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await patternWebPageDS.UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public async Task UpdatePhrase(MPatternPhrase item) =>
            await patternPhraseDS.Update(item);
        public async Task SearchPhrases(int patternid)
        {
            PhraseItems = new ObservableCollection<MPatternPhrase>(await patternPhraseDS.GetDataByPatternId(patternid));
            this.RaisePropertyChanged(nameof(PhraseItems));
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
        async void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo) =>
            await Reindex(_ => { });
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
