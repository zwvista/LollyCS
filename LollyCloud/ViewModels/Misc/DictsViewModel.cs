using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class DictsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        DictionaryDataStore dictDS = new DictionaryDataStore();

        public ObservableCollection<MDictionary> Items { get; set; }

        public DictsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            Reload();
        }
        public void Reload() =>
            dictDS.GetDictsByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                Items = new ObservableCollection<MDictionary>(lst);
                this.RaisePropertyChanged(nameof(Items));
            });

        public async Task Update(MDictionary item) => await dictDS.Update(item);
        public async Task<int> Create(MDictionary item) => await dictDS.Create(item);
        public async Task Delete(int id) => await dictDS.Delete(id);

    }
}
