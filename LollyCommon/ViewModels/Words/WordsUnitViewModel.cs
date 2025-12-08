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
        UnitWordDataStore unitWordDS = new();
        LangWordDataStore langWordDS = new();

        public ObservableCollection<MUnitWord> WordItems { get; set; } = [];
        public bool NoFilter => string.IsNullOrEmpty(TextFilter) && TextbookFilter == 0;
        public bool IfEmpty { get; set; } = true;
        public string StatusText => $"{WordItems.Count} Words in {(inTextbook ? vmSettings.UNITINFO : vmSettings.LANGINFO)}";

        public WordsUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy, bool paged) : base(vmSettings, needCopy, paged)
        {
            this.inTextbook = inTextbook;
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                WordItems = new ObservableCollection<MUnitWord>(inTextbook ? await unitWordDS.GetDataByTextbookUnitPart(
                    vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO, TextFilter, ScopeFilter) :
                    await unitWordDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks, TextFilter, ScopeFilter, TextbookFilter,
                    Paged ? PageNo : null, Paged ? PageSize : null));
                this.RaisePropertyChanged(nameof(WordItems));
                IsBusy = false;
            });
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.TextbookFilter).Subscribe(_ => Reload());
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();

        public async Task Update(MUnitWord item)
        {
            var result = (await unitWordDS.Update(item)).Result;
            var o = await unitWordDS.GetDataById(item.ID, vmSettings.Textbooks);
            // new word
            bool b = result == "2" || result == "4";
            o?.CopyProperties(item);
            if (b || item.NOTE.IsEmpty())
                await GetNote(item);
        }
        public async Task Create(MUnitWord item)
        {
            int id = await unitWordDS.Create(item);
            var o = await unitWordDS.GetDataById(id, vmSettings.Textbooks);
            o?.CopyProperties(item);
            if (item.NOTE.IsEmpty())
                await GetNote(item);
            WordItems.Add(o);
        }

        public async Task Delete(MUnitWord item) =>
            await unitWordDS.Delete(item);

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= WordItems.Count; i++)
            {
                var item = WordItems[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await unitWordDS.UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public MUnitWord NewUnitWord()
        {
            var maxElem = WordItems.IsEmpty() ? null : WordItems.MaxByWithTies(o => (o.UNIT, o.PART, o.SEQNUM)).First();
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

        public async Task GetNote(MUnitWord item)
        {
            var note = await vmSettings.GetNote(item.WORD);
            item.NOTE = note;
            await langWordDS.UpdateNote(item.WORDID, item.NOTE);
        }
        public async Task ClearNote(MUnitWord item)
        {
            item.NOTE = SettingsViewModel.ZeroNote;
            await langWordDS.UpdateNote(item.WORDID, item.NOTE);
        }

        public async Task GetNotes(Action<int> oneComplete) =>
            await vmSettings.GetNotes(WordItems.Count, i => !IfEmpty || string.IsNullOrEmpty(WordItems[i].NOTE),
                async i =>
                {
                    await GetNote(WordItems[i]);
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
