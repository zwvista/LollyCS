using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class PhrasesBaseViewModel : WordsPhrasesBaseViewModel
    {
        [Reactive]
        public MPhraseInterface SelectedPhraseItem { get; set; }
        public bool HasSelectedPhraseItem { [ObservableAsProperty] get; }
        public string SelectedPhrase => SelectedPhraseItem?.PHRASE ?? "";
        public int SelectedPhraseID => SelectedPhraseItem?.PHRASEID ?? 0;
        public PhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            ScopeFilter = SettingsViewModel.ScopePhraseFilters[0];
            this.WhenAnyValue(x => x.SelectedPhraseItem, (MPhraseInterface v) => v != null).ToPropertyEx(this, x => x.HasSelectedPhraseItem);
        }
    }
}
