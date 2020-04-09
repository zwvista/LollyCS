using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
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
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        ObservableCollection<MLangWord> WordItemsAll { get; set; }
        ObservableCollection<MLangWord> WordItemsFiltered { get; set; }
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
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.Levelge0only).Subscribe(_ =>
            {
                WordItemsFiltered = string.IsNullOrEmpty(TextFilter) && !Levelge0only ? null :
                new ObservableCollection<MLangWord>(WordItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())) &&
                    (!Levelge0only || o.LEVEL >= 0)
                ));
                this.RaisePropertyChanged(nameof(WordItems));
            });
            Reload();
        }
        public void Reload() =>
            langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID).ToObservable().Subscribe(lst =>
            {
                WordItemsAll = new ObservableCollection<MLangWord>(lst);
                this.RaisePropertyChanged(nameof(WordItems));
            });

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task<int> Create(MLangWord item) => await langWordDS.Create(item);
        public async Task Delete(MLangWord item)
        {
            await langWordDS.Delete(item.ID);
            await wordFamiDS.Delete(item.FAMIID);
            await wordPhraseDS.DeleteByWordId(item.ID);
        }

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
