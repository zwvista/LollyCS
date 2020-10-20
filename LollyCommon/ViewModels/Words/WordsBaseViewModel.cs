using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsPhrasesBaseViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; }
        [Reactive]
        public string NewWord { get; set; } = "";
        [Reactive]
        public MWordInterface SelectedWordItem { get; set; }
        public bool HasSelectedWordItem { [ObservableAsProperty] get; }
        public string SelectedWord => SelectedWordItem?.WORD ?? "";
        public int SelectedWordID => SelectedWordItem?.WORDID ?? 0;
        [Reactive]
        public MPhraseInterface SelectedPhraseItem { get; set; }
        public bool HasSelectedPhraseItem { [ObservableAsProperty] get; }
        public string SelectedPhrase => SelectedPhraseItem?.PHRASE ?? "";
        public int SelectedPhraseID => SelectedPhraseItem?.PHRASEID ?? 0;
        [Reactive]
        public string ScopeFilter { get; set; }
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public int TextbookFilter { get; set; }
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public WordsPhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.SelectedWordItem, (MWordInterface v) => v != null).ToPropertyEx(this, x => x.HasSelectedWordItem);
            this.WhenAnyValue(x => x.SelectedPhraseItem, (MPhraseInterface v) => v != null).ToPropertyEx(this, x => x.HasSelectedPhraseItem);
        }
    }
    public class WordsBaseViewModel : WordsPhrasesBaseViewModel
    {
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; } = new ObservableCollection<MLangPhrase>();

        public WordsBaseViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            ScopeFilter = SettingsViewModel.ScopeWordFilters[0];
        }
        public async Task SearchPhrases(int wordid)
        {
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWordId(wordid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
