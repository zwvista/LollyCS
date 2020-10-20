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

        public PhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            ScopeFilter = SettingsViewModel.ScopePhraseFilters[0];
        }
        public async Task SearchWords(int phraseid)
        {
            WordItems = new ObservableCollection<MLangWord>(await wordPhraseDS.GetWordsByPhraseId(phraseid));
            this.RaisePropertyChanged(nameof(WordItems));
        }
    }
}
