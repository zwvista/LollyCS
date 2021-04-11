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
        LangWordDataStore langWordDS = new LangWordDataStore();
        protected WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        ObservableCollection<MLangWord> WordItemsAll { get; set; } = new ObservableCollection<MLangWord>();
        public ObservableCollection<MLangWord> WordItems { get; set; } = new ObservableCollection<MLangWord>();
        public string StatusText => $"{WordItems.Count} Words in {vmSettings.LANGINFO}";

        public WordsLangViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                WordItemsAll = new ObservableCollection<MLangWord>(await langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        protected virtual void ApplyFilters()
        {
            WordItems = string.IsNullOrEmpty(TextFilter) ? WordItemsAll : new ObservableCollection<MLangWord>(WordItemsAll.Where(o =>
                string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE).ToLower().Contains(TextFilter.ToLower())
            ));
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task Create(MLangWord item)
        {
            await langWordDS.Create(item);
            WordItemsAll.Add(item);
            ApplyFilters();
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

        public async Task RetrieveNote(MLangWord item)
        {
            var note = await vmSettings.RetrieveNote(item.WORD);
            item.NOTE = note;
            await langWordDS.UpdateNote(item.ID, item.NOTE);
        }
        public async Task ClearNote(MLangWord item)
        {
            item.NOTE = SettingsViewModel.ZeroNote;
            await langWordDS.UpdateNote(item.ID, item.NOTE);
        }

        public WordsLangViewModel(SettingsViewModel vmSettings) : base(vmSettings, false)
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
        protected override void ApplyFilters()
        {
            base.ApplyFilters();
            foreach (var o in WordItems)
                o.IsChecked = false;
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
