using ReactiveUI;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class WordsLangViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        LangWordDataStore langWordDS = new LangWordDataStore();

        public ObservableCollection<MLangWord> ItemsAll { get; set; }
        public ObservableCollection<MLangWord> ItemsFiltered { get; set; }
        public ObservableCollection<MLangWord> Items => ItemsFiltered ?? ItemsAll;
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
            o.ItemsAll = new ObservableCollection<MLangWord>(await o.langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            o.ApplyFilters();
            return o;
        }
        public void ApplyFilters()
        {
            if (string.IsNullOrEmpty(TextFilter) && !Levelge0only)
                ItemsFiltered = null;
            else
            {
                ItemsFiltered = ItemsAll;
                if (!string.IsNullOrEmpty(TextFilter))
                    ItemsFiltered = new ObservableCollection<MLangWord>(ItemsFiltered.Where(o => (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())));
                if (Levelge0only)
                    ItemsFiltered = new ObservableCollection<MLangWord>(ItemsFiltered.Where(o => o.LEVEL >= 0));
            }
            this.RaisePropertyChanged(nameof(Items));
        }

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task<int> Create(MLangWord item) => await langWordDS.Create(item);
        public async Task Delete(int id) => await langWordDS.Delete(id);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
