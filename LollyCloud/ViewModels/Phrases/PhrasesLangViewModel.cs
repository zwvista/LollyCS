using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCloud
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
        public string StatusText => $"{PhraseItems?.Count ?? 0} Phrases in {vmSettings.UNITINFO}";

        public PhrasesLangViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ =>
            {
                PhraseItemsFiltered = string.IsNullOrEmpty(TextFilter) ? null :
                new ObservableCollection<MLangPhrase>(PhraseItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION ?? "").ToLower().Contains(TextFilter.ToLower()))
                ));
                this.RaisePropertyChanged(nameof(PhraseItems));
            });
            this.WhenAnyValue(x => x.PhraseItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            Reload();
        }
        public void Reload() =>
            langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID).ToObservable().Subscribe(lst =>
            {
                PhraseItemsAll = new ObservableCollection<MLangPhrase>(lst);
                this.RaisePropertyChanged(nameof(PhraseItems));
            });

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
