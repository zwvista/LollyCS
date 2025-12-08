using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public partial class PatternsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();
        public ObservableCollection<MPattern> PatternItems { get; set; } = [];
        [Reactive]
        public partial string TextFilter { get; set; } = "";
        [Reactive]
        public partial string ScopeFilter { get; set; } = SettingsViewModel.ScopePatternFilters[0];
        bool NoFilter => string.IsNullOrEmpty(TextFilter);
        public string StatusText => $"{PatternItems.Count} Patterns in {vmSettings.LANGINFO}";
        public bool Paged { get; set; }
        [Reactive]
        public partial int PageNo { get; set; } = 1;
        [Reactive]
        public partial int PageSize { get; set; }
        [Reactive]
        public partial MPattern SelectedPatternItem { get; set; }
        [ObservableAsProperty]
        public partial bool HasSelectedPatternItem { get; }
        public string SelectedPattern => SelectedPatternItem?.PATTERN ?? "";
        public int SelectedPatternID => SelectedPatternItem?.PATTERNID ?? 0;
        [Reactive]
        public partial bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public PatternsViewModel(SettingsViewModel vmSettings, bool needCopy, bool paged)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            Paged = paged;
            PageSize = vmSettings.USROWSPERPAGE;
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PatternItems = new ObservableCollection<MPattern>(
                    await patternDS.GetDataByLang(vmSettings.SelectedLang.ID, TextFilter, ScopeFilter,
                    Paged ? PageNo : null, Paged ? PageSize : null));
                this.RaisePropertyChanged(nameof(PatternItems));
                IsBusy = false;
            });
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => Reload());
            this.WhenAnyValue(x => x.PatternItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedPatternItem, (MPattern v) => v != null).ToProperty(this, x => x.HasSelectedPatternItem);
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();

        public async Task Update(MPattern item) => await patternDS.Update(item);
        public async Task Create(MPattern item)
        {
            item.ID = await patternDS.Create(item);
            PatternItems.Add(item);
        }
        public async Task Delete(int id) => await patternDS.Delete(id);

        public MPattern NewPattern() =>
            new MPattern
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

    }
}
