﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PhrasesLangViewModel : PhrasesBaseViewModel
    {
        LangPhraseDataStore langPhraseDS = new();
        protected WordPhraseDataStore wordPhraseDS = new();

        ObservableCollection<MLangPhrase> PhraseItemsAll { get; set; } = [];
        public ObservableCollection<MLangPhrase> PhraseItems { get; set; } = [];
        public string StatusText => $"{PhraseItems.Count} Phrases in {vmSettings.LANGINFO}";

        public PhrasesLangViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PhraseItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PhraseItemsAll = new ObservableCollection<MLangPhrase>(await langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        protected virtual void ApplyFilters()
        {
            PhraseItems = string.IsNullOrEmpty(TextFilter) ? PhraseItemsAll : new ObservableCollection<MLangPhrase>(PhraseItemsAll.Where(o =>
                string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION).ToLower().Contains(TextFilter.ToLower())
            ));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }

        public async Task Update(MLangPhrase item) => await langPhraseDS.Update(item);
        public async Task Create(MLangPhrase item)
        {
            item.ID = await langPhraseDS.Create(item);
            PhraseItemsAll.Add(item);
            ApplyFilters();
        }
        public async Task Delete(MLangPhrase item) => await langPhraseDS.Delete(item);

        public MLangPhrase NewLangPhrase() =>
            new MLangPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

        public PhrasesLangViewModel(SettingsViewModel vmSettings) : base(vmSettings, false)
        {
        }
        public async Task GetPhrases(int wordid)
        {
            IsBusy = true;
            PhraseItems = new ObservableCollection<MLangPhrase>(await wordPhraseDS.GetPhrasesByWordId(wordid));
            this.RaisePropertyChanged(nameof(PhraseItems));
            IsBusy = false;
        }
        public Task Dissociate(int wordid, int phraseid) =>
            wordPhraseDS.Dissociate(wordid, phraseid);
    }
    public class PhrasesAssociateViewModel : PhrasesLangViewModel
    {
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PhrasesAssociateViewModel(SettingsViewModel vmSettings, int wordid, string textFilter) : base(vmSettings, false)
        {
            TextFilter = textFilter;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in PhraseItems)
                    if (o.IsChecked)
                        await wordPhraseDS.Associate(wordid, o.ID);
            });
        }
        protected override void ApplyFilters()
        {
            base.ApplyFilters();
            foreach (var o in PhraseItems)
                o.IsChecked = false;
        }
        public void CheckItems(int n, List<MLangPhrase> selectedItems)
        {
            foreach (var o in PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }

}
