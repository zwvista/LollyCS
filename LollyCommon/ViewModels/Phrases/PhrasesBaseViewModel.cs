using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PhrasesBaseViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        public ObservableCollection<MLangWord> WordItems { get; set; } = new ObservableCollection<MLangWord>();
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];
        [Reactive]
        public int TextbookFilter { get; set; }
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public PhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }
        public async Task SearchWords(int phraseid)
        {
            WordItems = new ObservableCollection<MLangWord>(await wordPhraseDS.GetWordsByPhraseId(phraseid));
            this.RaisePropertyChanged(nameof(WordItems));
        }
    }
}
