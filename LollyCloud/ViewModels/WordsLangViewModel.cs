using ReactiveUI;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class WordsLangViewModel : BaseViewModel
    {
        public SettingsViewModel vmSettings;
        LangWordDataStore langWordDS = new LangWordDataStore();
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        public ObservableCollection<MLangWord> WordItemsAll { get; set; }
        public ObservableCollection<MLangWord> WordItemsFiltered { get; set; }
        public ObservableCollection<MLangWord> WordItems => WordItemsFiltered ?? WordItemsAll;
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; } = new ObservableCollection<MLangPhrase>();
        string _NewWord = "";
        public string NewWord
        {
            get => _NewWord;
            set => this.RaiseAndSetIfChanged(ref _NewWord, value);
        }
        string _TextFilter = "";
        public string TextFilter
        {
            get => _TextFilter;
            set => this.RaiseAndSetIfChanged(ref _TextFilter, value);
        }
        string _ScopeFilter = SettingsViewModel.ScopeWordFilters[0];
        public string ScopeFilter
        {
            get => _ScopeFilter;
            set => this.RaiseAndSetIfChanged(ref _ScopeFilter, value);
        }
        bool _Levelge0only;
        public bool Levelge0only
        {
            get => _Levelge0only;
            set => this.RaiseAndSetIfChanged(ref _Levelge0only, value);
        }

        public static async Task<WordsLangViewModel> CreateAsync(SettingsViewModel vmSettings, bool needCopy)
        {
            var o = new WordsLangViewModel();
            o.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            o.WordItemsAll = new ObservableCollection<MLangWord>(await o.langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            o.ApplyFilters();
            return o;
        }
        public void ApplyFilters()
        {
            if (string.IsNullOrEmpty(TextFilter) && !Levelge0only)
                WordItemsFiltered = null;
            else
            {
                WordItemsFiltered = WordItemsAll;
                if (!string.IsNullOrEmpty(TextFilter))
                    WordItemsFiltered = new ObservableCollection<MLangWord>(WordItemsFiltered.Where(o => (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())));
                if (Levelge0only)
                    WordItemsFiltered = new ObservableCollection<MLangWord>(WordItemsFiltered.Where(o => o.LEVEL >= 0));
            }
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task<int> Create(MLangWord item) => await langWordDS.Create(item);
        public async Task Delete(int id) => await langWordDS.Delete(id);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

        public async Task SearchPhrases(int wordid) =>
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWord(wordid));
    }
}
