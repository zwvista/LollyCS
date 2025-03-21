﻿using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class DictsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        DictionaryDataStore dictDS = new DictionaryDataStore();

        public ObservableCollection<MDictionary> Items { get; set; } = [];
        public string StatusText => $"{Items.Count} Dictionaries in {vmSettings.LANGINFO}";

        public DictsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.Items).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            Reload();
        }
        public void Reload() =>
            dictDS.GetDictsByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                Items = new ObservableCollection<MDictionary>(lst);
                this.RaisePropertyChanged(nameof(Items));
            });
        public MDictionary NewDictionary() =>
            new MDictionary
            {
                LANGIDFROM = vmSettings.SelectedLang.ID,
                LANGNAMEFROM = vmSettings.SelectedLang.LANGNAME,
                DICTTYPECODE = 3,
            };

        public void Add(MDictionary item)
        {
            Items.Add(item);
        }

        public async Task Update(MDictionary item) => await dictDS.Update(item);
        public async Task Create(MDictionary item) => item.ID = await dictDS.Create(item);
        public async Task Delete(int id) => await dictDS.Delete(id);
    }
}
