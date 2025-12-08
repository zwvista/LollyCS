using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsLangViewModel : WordsBaseViewModel
    {
        LangWordDataStore langWordDS = new();
        protected WordPhraseDataStore wordPhraseDS = new();

        public ObservableCollection<MLangWord> WordItems { get; set; } = new ObservableCollection<MLangWord>();
        public string StatusText => $"{WordItems.Count} Words in {vmSettings.LANGINFO}";

        public WordsLangViewModel(SettingsViewModel vmSettings, bool needCopy, bool paged) : base(vmSettings, needCopy, paged)
        {
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                WordItems = new ObservableCollection<MLangWord>(
                    await langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID, TextFilter, ScopeFilter,
                    Paged ? PageNo : null, Paged ? PageSize : null));
                this.RaisePropertyChanged(nameof(WordItems));
                IsBusy = false;
            });
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => Reload());
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task Create(MLangWord item)
        {
            await langWordDS.Create(item);
            WordItems.Add(item);
        }
        public async Task Delete(MLangWord item) => await langWordDS.Delete(item);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

        public async Task AddNewWord()
        {
            var item = NewLangWord();
            item.WORD = vmSettings.AutoCorrectInput(NewWord);
            NewWord = "";
            await Create(item);
            SelectedWordItem = WordItems.Last();
        }

        public async Task GetNote(MLangWord item)
        {
            var note = await vmSettings.GetNote(item.WORD);
            item.NOTE = note;
            await langWordDS.UpdateNote(item.ID, item.NOTE);
        }
        public async Task ClearNote(MLangWord item)
        {
            item.NOTE = SettingsViewModel.ZeroNote;
            await langWordDS.UpdateNote(item.ID, item.NOTE);
        }

        public WordsLangViewModel(SettingsViewModel vmSettings, bool paged) : base(vmSettings, false, paged)
        {
        }
        public async Task GetWords(int phraseid)
        {
            IsBusy = true;
            WordItems = new ObservableCollection<MLangWord>(await wordPhraseDS.GetWordsByPhraseId(phraseid));
            this.RaisePropertyChanged(nameof(WordItems));
            IsBusy = false;
            if (WordItems.Any())
                SelectedWordItem = WordItems[0];
        }
        public Task Dissociate(int wordid, int phraseid) =>
            wordPhraseDS.Dissociate(wordid, phraseid);
    }
    public class WordsAssociateViewModel : WordsLangViewModel
    {
        public ReactiveCommand<Unit, Unit> Save { get; }
        public WordsAssociateViewModel(SettingsViewModel vmSettings, int phraseid, string textFilter) : base(vmSettings, false)
        {
            TextFilter = textFilter;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in WordItems)
                    if (o.IsChecked)
                        await wordPhraseDS.Associate(phraseid, o.ID);
            });
        }
        public void CheckItems(int n, List<MLangWord> selectedItems)
        {
            foreach (var o in WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
