using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsLangViewModel : WordsBaseViewModel
    {
        LangWordDataStore langWordDS = new LangWordDataStore();
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

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
        void ApplyFilters()
        {
            WordItems = string.IsNullOrEmpty(TextFilter) ? WordItemsAll : new ObservableCollection<MLangWord>(WordItemsAll.Where(o =>
                string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())
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
        public Task Unlink(int wordid, int phraseid) =>
            wordPhraseDS.Unlink(wordid, phraseid);
    }
}
