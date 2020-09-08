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
    public class WordsLangViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangWordDataStore langWordDS = new LangWordDataStore();
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        List<MLangWord> WordItemsAll { get; set; } = new List<MLangWord>();
        public ObservableCollection<MLangWord> WordItems { get; set; } = new ObservableCollection<MLangWord>();
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; }
        [Reactive]
        public string NewWord { get; set; } = "";
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopeWordFilters[0];
        public string StatusText => $"{WordItems?.Count ?? 0} Words in {vmSettings.LANGINFO}";

        public WordsLangViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            Reload();
        }
        public void Reload() =>
            langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID).ToObservable().Subscribe(lst =>
            {
                WordItemsAll = lst;
                ApplyFilters();
            });
        void ApplyFilters()
        {
            WordItems = new ObservableCollection<MLangWord>(
                string.IsNullOrEmpty(TextFilter) ? WordItemsAll :
                WordItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower()))
                )
            );
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task Create(MLangWord item) => item.ID = await langWordDS.Create(item);
        public async Task Delete(MLangWord item) => await langWordDS.Delete(item);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

        public async Task SearchPhrases(int wordid)
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWordId(wordid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
