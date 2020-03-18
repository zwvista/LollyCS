using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyShared
{
    public class PhrasesLangViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        ObservableCollection<MLangPhrase> PhraseItemsAll { get; set; }
        ObservableCollection<MLangPhrase> PhraseItemsFiltered { get; set; }
        public ObservableCollection<MLangPhrase> PhraseItems => PhraseItemsFiltered ?? PhraseItemsAll;
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];

        public PhrasesLangViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID).ToObservable().Subscribe(lst =>
            {
                PhraseItemsAll = new ObservableCollection<MLangPhrase>(lst);
                this.RaisePropertyChanged(nameof(PhraseItems));
            });
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
        }
        void ApplyFilters()
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
