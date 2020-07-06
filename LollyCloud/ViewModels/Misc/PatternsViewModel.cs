using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class PatternsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();
        PatternWebPageDataStore patternWebPageDS = new PatternWebPageDataStore();
        PatternPhraseDataStore patternPhraseDS = new PatternPhraseDataStore();

        public ObservableCollection<MPattern> PatternItemsAll { get; set; }
        public ObservableCollection<MPattern> PatternItemsFiltered { get; set; }
        public ObservableCollection<MPattern> PatternItems => PatternItemsFiltered ?? PatternItemsAll;
        public ObservableCollection<MPatternWebPage> WebPageItems { get; set; }
        public ObservableCollection<MPatternPhrase> PhraseItems { get; set; }
        [Reactive]
        public string TextFilter { get; set; }
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePatternFilters[0];

        public PatternsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ =>
            {
                PatternItemsFiltered = string.IsNullOrEmpty(TextFilter) ? null :
                new ObservableCollection<MPattern>(PatternItemsAll.Where(o =>
                    (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Pattern" ? o.PATTERN : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower()))
                ));
                this.RaisePropertyChanged(nameof(PatternItems));
            });
            Reload();
        }
        public void Reload() =>
            patternDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PatternItemsAll = new ObservableCollection<MPattern>(lst);
                this.RaisePropertyChanged(nameof(PatternItems));
            });

        public async Task Update(MPattern item) => await patternDS.Update(item);
        public async Task<int> Create(MPattern item) => await patternDS.Create(item);
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
        public async Task UpdateWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Update(item);
        public async Task<int> CreateWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Create(item);
        public async Task DeleteWebPage(int id) =>
            await patternWebPageDS.Delete(id);
        public MPatternWebPage NewPatternWebPage(int patternid, string pattern) =>
            new MPatternWebPage
            {
                PATTERNID = patternid,
                PATTERN = pattern,
                SEQNUM = WebPageItems.Select(o => o.SEQNUM).StartWith(0).Max() + 1
            };

        public async Task UpdatePhrase(MPatternPhrase item) =>
            await patternPhraseDS.Update(item);
        public async Task SearchPhrases(int patternid)
        {
            PhraseItems = new ObservableCollection<MPatternPhrase>(await patternPhraseDS.GetDataByPatternId(patternid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
