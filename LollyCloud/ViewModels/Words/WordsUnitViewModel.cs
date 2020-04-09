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
    public class WordsUnitViewModel : ReactiveObject, IDragSource
    {
        public SettingsViewModel vmSettings;
        bool inTextbook;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        LangWordDataStore langWordDS = new LangWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();
        NoteViewModel vmNote;

        ObservableCollection<MUnitWord> WordItemsAll { get; set; }
        ObservableCollection<MUnitWord> WordItemsFiltered { get; set; }
        public ObservableCollection<MUnitWord> WordItems => WordItemsFiltered ?? WordItemsAll;
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; }
        [Reactive]
        public string NewWord { get; set; } = "";
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopeWordFilters[0];
        [Reactive]
        public bool Levelge0only { get; set; }
        [Reactive]
        public int TextbookFilter { get; set; }
        public bool IsEditing { get; set; }

        public WordsUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.inTextbook = inTextbook;
            vmNote = new NoteViewModel(vmSettings);
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.Levelge0only, x => x.TextbookFilter).Subscribe(_ =>
            {
                WordItemsFiltered = string.IsNullOrEmpty(TextFilter) && !Levelge0only && TextbookFilter == 0 ? null :
                new ObservableCollection<MUnitWord>(WordItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())) &&
                    (!Levelge0only || o.LEVEL >= 0) &&
                    (TextbookFilter == 0 || o.TEXTBOOKID == TextbookFilter)
                ));
                this.RaisePropertyChanged(nameof(WordItems));
            });
            Reload();
        }
        public void Reload() =>
            (inTextbook ? unitWordDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                unitWordDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks))
            .ToObservable().Subscribe(lst =>
            {
                WordItemsAll = new ObservableCollection<MUnitWord>(lst);
                this.RaisePropertyChanged(nameof(WordItems));
            });

        public async Task Update(MUnitWord item)
        {
            var wordid = item.WORDID;
            var lstUnit = await unitWordDS.GetDataByWordId(wordid);
            if (lstUnit.IsEmpty()) return;
            var itemLang = new MLangWord(item);
            var lstLangOld = await langWordDS.GetDataById(wordid);
            if (lstLangOld.Any() && lstLangOld[0].WORD == item.WORD)
            {
                await langWordDS.UpdateNote(wordid, item.NOTE);
                return;
            }
            var lstLangNew = await langWordDS.GetDataByLangWord(item.LANGID, item.WORD);
            async Task f()
            {
                itemLang = lstLangNew[0];
                wordid = itemLang.ID;
                var b = itemLang.CombineNote(item.NOTE);
                item.NOTE = itemLang.NOTE;
                var lstFami = await wordFamiDS.GetDataByUserWord(CommonApi.UserId, wordid);
                if (lstFami.Any())
                {
                    item.CORRECT = lstFami[0].CORRECT;
                    item.TOTAL = lstFami[0].TOTAL;
                }
                if (b) await langWordDS.UpdateNote(wordid, item.NOTE);
            }
            if (lstUnit.Count == 1)
                if (lstLangNew.IsEmpty())
                    await langWordDS.Update(itemLang);
                else
                {
                    await langWordDS.Delete(wordid);
                    await f();
                }
            else if (lstLangNew.IsEmpty())
            {
                itemLang.ID = 0;
                await langWordDS.Create(itemLang);
            }
            else
                await f();
            item.WORDID = wordid;
            await unitWordDS.Update(item);
        }
        public async Task<int> Create(MUnitWord item)
        {
            var lstLang = await langWordDS.GetDataByLangWord(item.LANGID, item.WORD);
            int wordid;
            if (lstLang.IsEmpty())
            {
                var itemLang = new MLangWord(item);
                wordid = await langWordDS.Create(itemLang);
            }
            else
            {
                var itemLang = lstLang[0];
                wordid = itemLang.ID;
                var b = itemLang.CombineNote(item.NOTE);
                item.NOTE = itemLang.NOTE;
                var lstFami = await wordFamiDS.GetDataByUserWord(CommonApi.UserId, wordid);
                if (lstFami.Any())
                {
                    item.CORRECT = lstFami[0].CORRECT;
                    item.TOTAL = lstFami[0].TOTAL;
                }
                if (b) await langWordDS.UpdateNote(wordid, item.NOTE);
            }
            item.WORDID = wordid;
            return await unitWordDS.Create(item);
        }

        public void Add(MUnitWord item)
        {
            WordItemsAll.Add(item);
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Delete(MUnitWord item)
        {
            await unitWordDS.Delete(item.ID);
            var lst = await unitWordDS.GetDataByWordId(item.WORDID);
            if (lst.IsEmpty())
                await langWordDS.Delete(item.WORDID);
        }

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= WordItemsAll.Count; i++)
            {
                var item = WordItemsAll[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await unitWordDS.UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public MUnitWord NewUnitWord()
        {
            var maxElem = WordItemsAll.IsEmpty() ? null : WordItemsAll.MaxBy(o => (o.UNIT, o.PART, o.SEQNUM)).First();
            return new MUnitWord
            {
                LANGID = vmSettings.SelectedLang.ID,
                TEXTBOOKID = vmSettings.USTEXTBOOKID,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
        }

        public async Task GetNote(int index)
        {
            var item = WordItemsAll[index];
            var note = await vmNote.GetNote(item.WORD);
            item.NOTE = note;
            await Update(item);
        }
        public async Task ClearNote(int index)
        {
            var item = WordItemsAll[index];
            item.NOTE = NoteViewModel.ZeroNote;
            await Update(item);
        }

        public async Task GetNotes(bool ifEmpty, Action<int> oneComplete, Action allComplete) =>
            await vmNote.GetNotes(WordItemsAll.Count, i => !ifEmpty || string.IsNullOrEmpty(WordItemsAll[i].NOTE),
                async i =>
                {
                    await GetNote(i);
                    oneComplete(i);
                }, allComplete);
        public async Task ClearNotes(bool ifEmpty, Action<int> oneComplete, Action allComplete) =>
            await vmNote.GetNotes(WordItemsAll.Count, i => !ifEmpty || string.IsNullOrEmpty(WordItemsAll[i].NOTE),
                async i =>
                {
                    await ClearNote(i);
                    oneComplete(i);
                }, allComplete);

        public async Task SearchPhrases(int wordid)
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWord(wordid));
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
        bool IDragSource.CanStartDrag(IDragInfo dragInfo) => !IsEditing && vmSettings.IsSingleUnitPart && WordItemsFiltered == null;
        void IDragSource.Dropped(IDropInfo dropInfo) { }
        async void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo) =>
            await Reindex(_ => { });
        void IDragSource.DragCancelled() { }
        bool IDragSource.TryCatchOccurredException(Exception exception) => false;
    }
}
