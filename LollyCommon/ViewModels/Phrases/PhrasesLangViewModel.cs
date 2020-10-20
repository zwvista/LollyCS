using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PhrasesLangViewModel : PhrasesBaseViewModel
    {
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        List<MLangPhrase> PhraseItemsAll { get; set; } = new List<MLangPhrase>();
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; } = new ObservableCollection<MLangPhrase>();
        public string StatusText => $"{PhraseItems.Count} Phrases in {vmSettings.LANGINFO}";

        public PhrasesLangViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PhraseItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PhraseItemsAll = await langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID);
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        void ApplyFilters()
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(string.IsNullOrEmpty(TextFilter) ? PhraseItemsAll : PhraseItemsAll.Where(o =>
                string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION ?? "").ToLower().Contains(TextFilter.ToLower())
            ));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }

        public async Task Update(MLangPhrase item) => await langPhraseDS.Update(item);
        public async Task Create(MLangPhrase item)
        {
            item.ID = await langPhraseDS.Create(item);
            PhraseItemsAll.Add(item);
            ApplyFilters();
        }
        public async Task Delete(MLangPhrase item) => await langPhraseDS.Delete(item);

        public MLangPhrase NewLangPhrase() =>
            new MLangPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
