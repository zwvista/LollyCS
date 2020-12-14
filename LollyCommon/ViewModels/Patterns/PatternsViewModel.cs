using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PatternsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();
        PatternWebPageDataStore patternWebPageDS = new PatternWebPageDataStore();
        WebPageDataStore webPageDS = new WebPageDataStore();

        ObservableCollection<MPattern> PatternItemsAll { get; set; } = new ObservableCollection<MPattern>();
        public ObservableCollection<MPattern> PatternItems { get; set; } = new ObservableCollection<MPattern>();
        public ObservableCollection<MPatternWebPage> WebPageItems { get; set; }
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePatternFilters[0];
        bool NoFilter => string.IsNullOrEmpty(TextFilter);
        public string StatusText => $"{PatternItems.Count} Patterns in {vmSettings.LANGINFO}";
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public PatternsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PatternItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
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

        public async Task GetWebPages(int patternid)
        {
            WebPageItems = new ObservableCollection<MPatternWebPage>(await patternWebPageDS.GetDataByPattern(patternid));
            this.RaisePropertyChanged(nameof(WebPageItems));
        }
        public async Task UpdatePatternWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Update(item);
        public async Task CreatePatternWebPage(MPatternWebPage item)
        {
            item.ID = await patternWebPageDS.Create(item);
            WebPageItems.Add(item);
        }
        public async Task DeletePatternWebPage(int id) =>
            await patternWebPageDS.Delete(id);
        public async Task UpdateWebPage(MPatternWebPage item) =>
            await webPageDS.Update(item);
        public async Task CreateWebPage(MPatternWebPage item) =>
            item.WEBPAGEID = await webPageDS.Create(item);
        public async Task DeleteWebPage(int id) =>
            await webPageDS.Delete(id);
        public MPatternWebPage NewPatternWebPage(int patternid, string pattern) =>
            new MPatternWebPage
            {
                PATTERNID = patternid,
                PATTERN = pattern,
                SEQNUM = WebPageItems.Select(o => o.SEQNUM).StartWith(0).Max() + 1
            };

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= WebPageItems.Count; i++)
            {
                var item = WebPageItems[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await patternWebPageDS.UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }
    }
}
