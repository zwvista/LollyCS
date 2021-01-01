using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PatternsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();
        ObservableCollection<MPattern> PatternItemsAll { get; set; } = new ObservableCollection<MPattern>();
        public ObservableCollection<MPattern> PatternItems { get; set; } = new ObservableCollection<MPattern>();
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePatternFilters[0];
        bool NoFilter => string.IsNullOrEmpty(TextFilter);
        public string StatusText => $"{PatternItems.Count} Patterns in {vmSettings.LANGINFO}";
        [Reactive]
        public MPattern SelectedPatternItem { get; set; }
        public bool HasSelectedPatternItem { [ObservableAsProperty] get; }
        public string SelectedPattern => SelectedPatternItem?.PATTERN ?? "";
        public int SelectedPatternID => SelectedPatternItem?.PATTERNID ?? 0;
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public PatternsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PatternItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedPatternItem, (MPattern v) => v != null).ToPropertyEx(this, x => x.HasSelectedPatternItem);
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PatternItemsAll = new ObservableCollection<MPattern>(await patternDS.GetDataByLang(vmSettings.SelectedLang.ID));
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        void ApplyFilters()
        {
            PatternItems = NoFilter ? PatternItemsAll : new ObservableCollection<MPattern>(PatternItemsAll.Where(o =>
                (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Pattern" ? o.PATTERN : ScopeFilter == "Note" ? o.NOTE ?? "" : o.TAGS ?? "").ToLower().Contains(TextFilter.ToLower()))
            ));
            this.RaisePropertyChanged(nameof(PatternItems));
        }

        public async Task Update(MPattern item) => await patternDS.Update(item);
        public async Task Create(MPattern item)
        {
            item.ID = await patternDS.Create(item);
            PatternItemsAll.Add(item);
            ApplyFilters();
        }
        public async Task Delete(int id) => await patternDS.Delete(id);

        public MPattern NewPattern() =>
            new MPattern
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

    }
}
