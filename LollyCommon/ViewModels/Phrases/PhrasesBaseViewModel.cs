using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public partial class PhrasesBaseViewModel : WordsPhrasesBaseViewModel
    {
        [Reactive]
        public partial MPhraseInterface SelectedPhraseItem { get; set; }
        [ObservableAsProperty]
        public partial bool HasSelectedPhraseItem { get; }
        public string SelectedPhrase => SelectedPhraseItem?.PHRASE ?? "";
        public int SelectedPhraseID => SelectedPhraseItem?.PHRASEID ?? 0;
        public PhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            ScopeFilter = SettingsViewModel.ScopePhraseFilters[0];
            this.WhenAnyValue(x => x.SelectedPhraseItem, (MPhraseInterface v) => v != null).ToProperty(this, x => x.HasSelectedPhraseItem);
        }
    }
}
