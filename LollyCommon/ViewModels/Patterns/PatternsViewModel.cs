using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCommon
{
    public class PatternsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        PatternDataStore patternDS = new PatternDataStore();
        PatternWebPageDataStore patternWebPageDS = new PatternWebPageDataStore();
        WebPageDataStore webPageDS = new WebPageDataStore();
        PatternPhraseDataStore patternPhraseDS = new PatternPhraseDataStore();

        List<MPattern> PatternItemsAll { get; set; } = new List<MPattern>();
        public ObservableCollection<MPattern> PatternItems { get; set; } = new ObservableCollection<MPattern>();
        public ObservableCollection<MPatternWebPage> WebPageItems { get; set; }
        public ObservableCollection<MPatternPhrase> PhraseItems { get; set; }
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePatternFilters[0];
        bool NoFilter => string.IsNullOrEmpty(TextFilter);
        public string StatusText => $"{PatternItems.Count} Patterns in {vmSettings.LANGINFO}";

        public PatternsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PatternItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            Reload();
        }
        public void Reload() =>
            patternDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PatternItemsAll = lst;
                ApplyFilters();
            });
        void ApplyFilters()
        {
            PatternItems = new ObservableCollection<MPattern>(NoFilter ? PatternItemsAll : PatternItemsAll.Where(o =>
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

        public async Task UpdatePhrase(MPatternPhrase item) =>
            await patternPhraseDS.Update(item);
        public async Task SearchPhrases(int patternid)
        {
            PhraseItems = new ObservableCollection<MPatternPhrase>(await patternPhraseDS.GetDataByPatternId(patternid));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
    }
}
