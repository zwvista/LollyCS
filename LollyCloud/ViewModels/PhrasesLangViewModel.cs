using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class PhrasesLangViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        public ObservableCollection<MLangPhrase> PhraseItemsAll { get; set; }
        public ObservableCollection<MLangPhrase> PhraseItemsFiltered { get; set; }
        public ObservableCollection<MLangPhrase> PhraseItems => PhraseItemsFiltered ?? PhraseItemsAll;
        string _TextFilter = "";
        public string TextFilter
        {
            get => _TextFilter;
            set => this.RaiseAndSetIfChanged(ref _TextFilter, value);
        }
        string _ScopeFilter = SettingsViewModel.ScopePhraseFilters[0];
        public string ScopeFilter
        {
            get => _ScopeFilter;
            set => this.RaiseAndSetIfChanged(ref _ScopeFilter, value);
        }

        public static async Task<PhrasesLangViewModel> CreateAsync(SettingsViewModel vmSettings, bool needCopy)
        {
            var o = new PhrasesLangViewModel();
            o.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            o.PhraseItemsAll = new ObservableCollection<MLangPhrase>(await o.langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            o.ApplyFilters();
            return o;
        }
        public void ApplyFilters()
        {
            if (string.IsNullOrEmpty(TextFilter))
                PhraseItemsFiltered = null;
            else
            {
                PhraseItemsFiltered = PhraseItemsAll;
                if (!string.IsNullOrEmpty(TextFilter))
                    PhraseItemsFiltered = new ObservableCollection<MLangPhrase>(PhraseItemsFiltered.Where(o => (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION ?? "").ToLower().Contains(TextFilter.ToLower())));
            }
            this.RaisePropertyChanged(nameof(PhraseItems));
        }

        public async Task Update(MLangPhrase item) => await langPhraseDS.Update(item);
        public async Task<int> Create(MLangPhrase item) => await langPhraseDS.Create(item);
        public async Task Delete(int id) => await langPhraseDS.Delete(id);

        public MLangPhrase NewLangPhrase() =>
            new MLangPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
