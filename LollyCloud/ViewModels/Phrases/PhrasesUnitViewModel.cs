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

namespace LollyShared
{
    public class PhrasesUnitViewModel : ReactiveObject, IDragSource
    {
        public SettingsViewModel vmSettings;
        bool inTextbook;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        ObservableCollection<MUnitPhrase> PhraseItemsAll { get; set; }
        ObservableCollection<MUnitPhrase> PhraseItemsFiltered { get; set; }
        public ObservableCollection<MUnitPhrase> PhraseItems => PhraseItemsFiltered ?? PhraseItemsAll;
        public bool CanReorder => vmSettings.IsSingleUnitPart && PhraseItemsFiltered == null;
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];
        [Reactive]
        public int TextbookFilter { get; set; }
        public bool IsEditing { get; set; }

        public PhrasesUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.inTextbook = inTextbook;
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.TextbookFilter).Subscribe(_ =>
            {
                PhraseItemsFiltered = string.IsNullOrEmpty(TextFilter) && TextbookFilter == 0 ? null :
                new ObservableCollection<MUnitPhrase>(PhraseItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION ?? "").ToLower().Contains(TextFilter.ToLower())) &&
                    (TextbookFilter == 0 || o.TEXTBOOKID == TextbookFilter)
                ));
                this.RaisePropertyChanged(nameof(PhraseItems));
            });
            Reload();
        }
        public void Reload() =>
            (inTextbook ? unitPhraseDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                unitPhraseDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks))
            .ToObservable().Subscribe(lst =>
            {
                PhraseItemsAll = new ObservableCollection<MUnitPhrase>();
                this.RaisePropertyChanged(nameof(PhraseItems));
            });

        public async Task UpdateSeqNum(int id, int seqnum) => await unitPhraseDS.UpdateSeqNum(id, seqnum);
        public async Task Update(MUnitPhrase item)
        {
            var phraseid = item.PHRASEID;
            var lstUnit = await unitPhraseDS.GetDataByLangPhrase(phraseid);
            if (lstUnit.IsEmpty()) return;
            var itemLang = new MLangPhrase(item);
            var lstLangOld = await langPhraseDS.GetDataById(phraseid);
            if (!lstLangOld.IsEmpty() && lstLangOld[0].PHRASE == item.PHRASE)
            {
                await langPhraseDS.UpdateTranslation(phraseid, item.TRANSLATION);
                return;
            }
            var lstLangNew = await langPhraseDS.GetDataByLangPhrase(item.LANGID, item.PHRASE);
            async Task f()
            {
                itemLang = lstLangNew[0];
                phraseid = itemLang.ID;
                var b = itemLang.CombineTranslation(item.TRANSLATION);
                item.TRANSLATION = itemLang.TRANSLATION;
                if (b) await langPhraseDS.UpdateTranslation(phraseid, item.TRANSLATION);
            }
            if (lstUnit.Count == 1)
                if (lstLangNew.IsEmpty())
                    await langPhraseDS.Update(itemLang);
                else
                {
                    await langPhraseDS.Delete(phraseid);
                    await f();
                }
            else if (lstLangNew.IsEmpty())
            {
                itemLang.ID = 0;
                await langPhraseDS.Create(itemLang);
            }
            else
                await f();
            item.PHRASEID = phraseid;
            await unitPhraseDS.Update(item);
        }
        public async Task<int> Create(MUnitPhrase item)
        {
            var lstLang = await langPhraseDS.GetDataByLangPhrase(item.LANGID, item.PHRASE);
            int phraseid;
            if (lstLang.IsEmpty())
            {
                var itemLang = new MLangPhrase(item);
                phraseid = await langPhraseDS.Create(itemLang);
            }
            else
            {
                var itemLang = lstLang[0];
                phraseid = itemLang.ID;
                var b = itemLang.CombineTranslation(item.TRANSLATION);
                item.TRANSLATION = itemLang.TRANSLATION;
                if (b) await langPhraseDS.UpdateTranslation(phraseid, item.TRANSLATION);
            }
            item.PHRASEID = phraseid;
            return await unitPhraseDS.Create(item);
        }
        public async Task Delete(MUnitPhrase item)
        {
            await unitPhraseDS.Delete(item.ID);
            var lst = await unitPhraseDS.GetDataByLangPhrase(item.PHRASEID);
            if (lst.IsEmpty())
                await langPhraseDS.Delete(item.PHRASEID);
        }

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= PhraseItemsAll.Count; i++)
            {
                var item = PhraseItemsAll[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public MUnitPhrase NewUnitPhrase()
        {
            var maxElem = PhraseItemsAll.MaxBy(o => (o.UNIT, o.PART, o.SEQNUM)).FirstOrDefault();
            return new MUnitPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
                TEXTBOOKID = maxElem?.UNIT ?? vmSettings.USUNITTO,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
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
        bool IDragSource.CanStartDrag(IDragInfo dragInfo) => !IsEditing && CanReorder;
        void IDragSource.Dropped(IDropInfo dropInfo) { }
        async void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo) =>
            await Reindex(_ => { });
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
