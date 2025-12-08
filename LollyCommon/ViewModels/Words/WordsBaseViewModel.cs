using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public partial class WordsPhrasesBaseViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; }
        [Reactive]
        public partial string ScopeFilter { get; set; }
        [Reactive]
        public partial string TextFilter { get; set; } = "";
        [Reactive]
        public partial int TextbookFilter { get; set; }
        public MSelectItem TextbookFilterItem
        {
            get => vmSettings.TextbookFilters.SingleOrDefault(o => o.Value == TextbookFilter);
            set { if (value != null) TextbookFilter = value.Value; }
        }
        public bool paginated { get; set; }
        [Reactive]
        public partial int PageNo { get; set; } = 1;
        [Reactive]
        public partial int PageSize { get; set; }
        [Reactive]
        public partial bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public WordsPhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy, bool paginated)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            paginated = paginated;
            PageSize = vmSettings.USROWSPERPAGE;
        }
    }
    public partial class WordsBaseViewModel : WordsPhrasesBaseViewModel
    {
        [Reactive]
        public partial string NewWord { get; set; } = "";
        [Reactive]
        public partial MWordInterface SelectedWordItem { get; set; } = null!;
        [ObservableAsProperty]
        public partial bool HasSelectedWordItem { get; }
        public virtual string SelectedWord => SelectedWordItem?.WORD ?? "";
        public int SelectedWordID => SelectedWordItem?.WORDID ?? 0;
        public WordsBaseViewModel(SettingsViewModel vmSettings, bool needCopy, bool paginated) : base(vmSettings, needCopy, paginated)
        {
            ScopeFilter = SettingsViewModel.ScopeWordFilters[0];
            this.WhenAnyValue(x => x.SelectedWordItem, (MWordInterface v) => v != null).ToProperty(this, x => x.HasSelectedWordItem);
        }
    }
}
