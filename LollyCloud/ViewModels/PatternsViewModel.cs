﻿using ReactiveUI;
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
        PatternWebPageDataStore patternWebPageDS = new PatternWebPageDataStore();
        PatternPhraseDataStore patternPhraseDS = new PatternPhraseDataStore();

        public ObservableCollection<MPattern> PatternItemsAll { get; set; }
        public ObservableCollection<MPattern> PatternItemsFiltered { get; set; }
        public ObservableCollection<MPattern> PatternItems => PatternItemsFiltered ?? PatternItemsAll;
        public ObservableCollection<MPatternWebPage> WebPageItems { get; set; }
        public ObservableCollection<MPatternPhrase> PhraseItems { get; set; }
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

        public async Task GetWebPages(int patternid) =>
            WebPageItems = new ObservableCollection<MPatternWebPage>(await patternWebPageDS.GetDataByPattern(patternid));
        public async Task UpdateWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Update(item);
        public async Task CreateWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Create(item);
        public async Task DeleteWebPage(int id) =>
            await patternWebPageDS.Delete(id);
        public MPatternWebPage NewPatternWebPage(int patternid, string pattern) =>
            new MPatternWebPage
            {
                PATTERNID = patternid,
                PATTERN = pattern,
                SEQNUM = (WebPageItems.MaxBy(o => o.SEQNUM).FirstOrDefault()?.SEQNUM ?? 0) + 1
            };

        public async Task UpdatePhrase(MPatternPhrase item) =>
            await patternPhraseDS.Update(item);
        public async Task SearchPhrases(int patternid) =>
            PhraseItems = new ObservableCollection<MPatternPhrase>(await patternPhraseDS.GetDataByPatternId(patternid));
    }
}
