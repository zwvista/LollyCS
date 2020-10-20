using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsBaseViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; }
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; } = new ObservableCollection<MLangPhrase>();
        [Reactive]
        public string NewWord { get; set; } = "";
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopeWordFilters[0];
        [Reactive]
        public int TextbookFilter { get; set; }
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public WordsBaseViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }
        public async Task SearchPhrases(int wordid)
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWordId(wordid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
