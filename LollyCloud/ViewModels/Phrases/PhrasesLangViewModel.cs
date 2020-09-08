using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
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

        List<MLangPhrase> PhraseItemsAll { get; set; } = new List<MLangPhrase>();
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; } = new ObservableCollection<MLangPhrase>();
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];
        public string StatusText => $"{PhraseItems.Count} Phrases in {vmSettings.LANGINFO}";

        public PhrasesLangViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PhraseItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            Reload();
        }
        public void Reload() =>
            langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID).ToObservable().Subscribe(lst =>
            {
                PhraseItemsAll = lst;
                ApplyFilters();
            });
        void ApplyFilters()
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(string.IsNullOrEmpty(TextFilter) ? PhraseItemsAll : PhraseItemsAll.Where(o =>
                string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION ?? "").ToLower().Contains(TextFilter.ToLower())
            ));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }

        public async Task Update(MLangPhrase item) => await langPhraseDS.Update(item);
        public async Task Create(MLangPhrase item) => item.ID = await langPhraseDS.Create(item);
        public async Task Delete(MLangPhrase item) => await langPhraseDS.Delete(item);

        public MLangPhrase NewLangPhrase() =>
            new MLangPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
