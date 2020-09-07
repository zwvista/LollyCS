using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class WordsUnitViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; }
        bool inTextbook;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
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
        public int TextbookFilter { get; set; }
        public bool IfEmpty { get; set; } = true;
        public string StatusText => $"{WordItems?.Count ?? 0} Words in {vmSettings.UNITINFO}";
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; }

        public WordsUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.inTextbook = inTextbook;
            vmNote = new NoteViewModel(vmSettings);
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.TextbookFilter).Subscribe(_ =>
            {
                WordItemsFiltered = string.IsNullOrEmpty(TextFilter) && TextbookFilter == 0 ? null :
                new ObservableCollection<MUnitWord>(WordItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())) &&
                    (TextbookFilter == 0 || o.TEXTBOOKID == TextbookFilter)
                ));
                this.RaisePropertyChanged(nameof(WordItems));
            });
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                var lst = inTextbook ? await unitWordDS.GetDataByTextbookUnitPart(
                    vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                    await unitWordDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks);
                WordItemsAll = new ObservableCollection<MUnitWord>(lst);
                this.RaisePropertyChanged(nameof(WordItems));
                IsBusy = false;
            });
        }

        public async Task<MUnitWord> Update(MUnitWord item)
        {
            await unitWordDS.Update(item);
            var o = await unitWordDS.GetDataById(item.ID, vmSettings.Textbooks);
            return o;
        }
        public async Task<MUnitWord> Create(MUnitWord item)
        {
            int id = await unitWordDS.Create(item);
            var o = await unitWordDS.GetDataById(id, vmSettings.Textbooks);
            return o;
        }

        public void Add(MUnitWord item)
        {
            WordItemsAll.Add(item);
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public void Replace(int index, MUnitWord item)
        {
            WordItems[index] = item;
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Delete(MUnitWord item) =>
            await unitWordDS.Delete(item);

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

        public async Task GetNotes(Action<int> oneComplete) =>
            await vmNote.GetNotes(WordItemsAll.Count, i => !IfEmpty || string.IsNullOrEmpty(WordItemsAll[i].NOTE),
                async i =>
                {
                    await GetNote(i);
                    oneComplete(i);
                });
        public async Task ClearNotes(Action<int> oneComplete) =>
            await vmNote.ClearNotes(WordItemsAll.Count, i => !IfEmpty || string.IsNullOrEmpty(WordItemsAll[i].NOTE),
                async i =>
                {
                    await ClearNote(i);
                    oneComplete(i);
                });

        public async Task SearchPhrases(int wordid)
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWordId(wordid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
