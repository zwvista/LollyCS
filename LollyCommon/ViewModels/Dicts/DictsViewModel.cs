using ReactiveUI;
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
        DictionaryDictDataStore dictDictDS = new DictionaryDictDataStore();
        DictionarySiteDataStore dictSiteDS = new DictionarySiteDataStore();

        public ObservableCollection<MDictionary> Items { get; set; } = new ObservableCollection<MDictionary>();
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
            };

        public void Add(MDictionary item)
        {
            Items.Add(item);
        }

        public async Task UpdateDict(MDictionaryDict item) => await dictDictDS.Update(item);
        public async Task CreateDict(MDictionaryDict item) => item.ID = await dictDictDS.Create(item);
        public async Task DeleteDict(int id) => await dictDictDS.Delete(id);
        public async Task UpdateSite(MDictionarySite item) => await dictSiteDS.Update(item);
        public async Task CreateSite(MDictionarySite item) => item.SITEID = await dictSiteDS.Create(item);
        public async Task DeleteSite(int id) => await dictSiteDS.Delete(id);

    }
}
