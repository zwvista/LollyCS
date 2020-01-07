using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class PatternsViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();

        public ObservableCollection<MPattern> PatternItemsAll { get; set; }
        public ObservableCollection<MPattern> PatternItemsFiltered { get; set; }
        public ObservableCollection<MPattern> PatternItems => PatternItemsFiltered ?? PatternItemsAll;
        string _TextFilter = "";
        public string TextFilter
        {
            get => _TextFilter;
            set => this.RaiseAndSetIfChanged(ref _TextFilter, value);
        }
        string _ScopeFilter = SettingsViewModel.ScopePatternFilters[0];
        public string ScopeFilter
        {
            get => _ScopeFilter;
            set => this.RaiseAndSetIfChanged(ref _ScopeFilter, value);
        }

        public static async Task<PatternsViewModel> CreateAsync(SettingsViewModel vmSettings, bool needCopy)
        {
            var o = new PatternsViewModel();
            o.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            o.PatternItemsAll = new ObservableCollection<MPattern>(await o.patternDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            o.ApplyFilters();
            return o;
        }
        public void ApplyFilters()
        {
            if (string.IsNullOrEmpty(TextFilter))
                PatternItemsFiltered = null;
            else
            {
                PatternItemsFiltered = PatternItemsAll;
                if (!string.IsNullOrEmpty(TextFilter))
                    PatternItemsFiltered = new ObservableCollection<MPattern>(PatternItemsFiltered.Where(o => (ScopeFilter == "Pattern" ? o.PATTERN : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())));
            }
            this.RaisePropertyChanged(nameof(PatternItems));
        }

        public async Task Update(MPattern item) => await patternDS.Update(item);
        public async Task<int> Create(MPattern item) => await patternDS.Create(item);
        public async Task Delete(int id) => await patternDS.Delete(id);

        public MPattern NewPattern() =>
            new MPattern
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
