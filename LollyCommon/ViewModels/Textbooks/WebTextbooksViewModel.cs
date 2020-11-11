using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WebTextbooksViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; set; }
        WebTextbookDataStore WebTextbookDS = new WebTextbookDataStore();
        [Reactive]
        public int WebTextbookFilter { get; set; }
        [Reactive]
        public MWebTextbook SelectedWebTextbookItem { get; set; }
        public bool HasSelectedWebTextbookItem { [ObservableAsProperty] get; }

        public ObservableCollection<MWebTextbook> ItemsAll { get; set; } = new ObservableCollection<MWebTextbook>();
        public ObservableCollection<MWebTextbook> Items { get; set; } = new ObservableCollection<MWebTextbook>();
        public bool NoFilter => WebTextbookFilter == 0;
        public string StatusText => $"{Items.Count} WebTextbooks in {vmSettings.LANGINFO}";

        public WebTextbooksViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.Items).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedWebTextbookItem, (MWebTextbook v) => v != null).ToPropertyEx(this, x => x.HasSelectedWebTextbookItem);
            Reload();
        }
        public void Reload() =>
            WebTextbookDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                ItemsAll = new ObservableCollection<MWebTextbook>(lst);
                ApplyFilters();
            });
        void ApplyFilters()
        {
            Items = NoFilter ? ItemsAll : new ObservableCollection<MWebTextbook>(ItemsAll.Where(o =>
                 (WebTextbookFilter == 0 || o.TEXTBOOKID == WebTextbookFilter)
            ));
            this.RaisePropertyChanged(nameof(Items));
        }
    }
}
