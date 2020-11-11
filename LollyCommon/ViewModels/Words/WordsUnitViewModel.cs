using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsUnitViewModel : WordsBaseViewModel
    {
        bool inTextbook;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        LangWordDataStore langWordDS = new LangWordDataStore();

        ObservableCollection<MUnitWord> WordItemsAll { get; set; } = new ObservableCollection<MUnitWord>();
        public ObservableCollection<MUnitWord> WordItems { get; set; } = new ObservableCollection<MUnitWord>();
        public bool NoFilter => string.IsNullOrEmpty(TextFilter) && TextbookFilter == 0;
        public bool IfEmpty { get; set; } = true;
        public string StatusText => $"{WordItems.Count} Words in {(inTextbook ? vmSettings.UNITINFO : vmSettings.LANGINFO)}";

        public WordsUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy) : base(vmSettings, needCopy)
        {
            this.inTextbook = inTextbook;
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.TextbookFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                WordItemsAll = new ObservableCollection<MUnitWord>(inTextbook ? await unitWordDS.GetDataByTextbookUnitPart(
                    vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                    await unitWordDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks));
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        void ApplyFilters()
        {
            WordItems = NoFilter ? WordItemsAll : new ObservableCollection<MUnitWord>(WordItemsAll.Where(o =>
                 (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())) &&
                 (TextbookFilter == 0 || o.TEXTBOOKID == TextbookFilter)
            ));
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Update(MUnitWord item)
        {
            await unitWordDS.Update(item);
            var o = await unitWordDS.GetDataById(item.ID, vmSettings.Textbooks);
            o?.CopyProperties(item);
        }
        public async Task Create(MUnitWord item)
        {
            int id = await unitWordDS.Create(item);
            var o = await unitWordDS.GetDataById(id, vmSettings.Textbooks);
            o?.CopyProperties(item);
            WordItemsAll.Add(o);
            ApplyFilters();
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
                TEXTBOOKID = vmSettings.USTEXTBOOK,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
        }

        public async Task AddNewWord()
        {
            var item = NewUnitWord();
            item.WORD = vmSettings.AutoCorrectInput(NewWord);
            NewWord = "";
            await Create(item);
            SelectedWordItem = WordItems.Last();
        }

        public async Task RetrieveNote(MUnitWord item)
        {
            var note = await vmSettings.RetrieveNote(item.WORD);
            item.NOTE = note;
            await langWordDS.UpdateNote(item.WORDID, item.NOTE);
        }
        public async Task ClearNote(MUnitWord item)
        {
            item.NOTE = SettingsViewModel.ZeroNote;
            await langWordDS.UpdateNote(item.WORDID, item.NOTE);
        }

        public async Task RetrieveNotes(Action<int> oneComplete) =>
            await vmSettings.RetrieveNotes(WordItems.Count, i => !IfEmpty || string.IsNullOrEmpty(WordItems[i].NOTE),
                async i =>
                {
                    await RetrieveNote(WordItems[i]);
                    oneComplete(i);
                });
        public async Task ClearNotes(Action<int> oneComplete) =>
            await vmSettings.ClearNotes(WordItems.Count, i => !IfEmpty || string.IsNullOrEmpty(WordItems[i].NOTE),
                async i =>
                {
                    await ClearNote(WordItems[i]);
                    oneComplete(i);
                });

    }
}
