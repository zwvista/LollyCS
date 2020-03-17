using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyShared
{
    public class WordsLangViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangWordDataStore langWordDS = new LangWordDataStore();
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        public ObservableCollection<MLangWord> WordItemsAll { get; set; }
        public ObservableCollection<MLangWord> WordItemsFiltered { get; set; }
        public ObservableCollection<MLangWord> WordItems => WordItemsFiltered ?? WordItemsAll;
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; }
        [Reactive]
        public string NewWord { get; set; } = "";
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopeWordFilters[0];
        [Reactive]
        public bool Levelge0only { get; set; }

        public WordsLangViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID).ToObservable().Subscribe(lst =>
            {
                WordItemsAll = new ObservableCollection<MLangWord>(lst);
                this.RaisePropertyChanged(nameof(WordItems));
            });
            ApplyFilters();
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

        public async Task SearchPhrases(int wordid)
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWord(wordid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
