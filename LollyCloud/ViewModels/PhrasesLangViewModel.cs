using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LollyShared
{
    public class PhrasesLangViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        public ObservableCollection<MLangPhrase> PhraseItemsAll { get; set; }
        public ObservableCollection<MLangPhrase> PhraseItemsFiltered { get; set; }
        public ObservableCollection<MLangPhrase> PhraseItems => PhraseItemsFiltered ?? PhraseItemsAll;
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];

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
