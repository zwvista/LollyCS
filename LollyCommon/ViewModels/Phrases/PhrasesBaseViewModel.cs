using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PhrasesBaseViewModel : WordsPhrasesBaseViewModel
    {
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        public ObservableCollection<MLangWord> WordItems { get; set; } = new ObservableCollection<MLangWord>();
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];

        public PhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
        }
        public async Task SearchWords(int phraseid)
        {
            WordItems = new ObservableCollection<MLangWord>(await wordPhraseDS.GetWordsByPhraseId(phraseid));
            this.RaisePropertyChanged(nameof(WordItems));
        }
    }
}
