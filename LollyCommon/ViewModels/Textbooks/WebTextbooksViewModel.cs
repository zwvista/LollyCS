using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Joins;
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
        public MSelectItem WebTextbookFilterItem
        {
            get => vmSettings.WebTextbookFilters.SingleOrDefault(o => o.Value == WebTextbookFilter);
            set { if (value != null) WebTextbookFilter = value.Value; }
        }
        [Reactive]
        public MWebTextbook SelectedWebTextbookItem { get; set; }
        public bool HasSelectedWebTextbookItem { [ObservableAsProperty] get; }

        public ObservableCollection<MWebTextbook> ItemsAll { get; set; } = new ObservableCollection<MWebTextbook>();
        public ObservableCollection<MWebTextbook> Items { get; set; } = new ObservableCollection<MWebTextbook>();
        public bool NoFilter => WebTextbookFilter == 0;
        public string StatusText => $"{Items.Count} WebTextbooks in {vmSettings.LANGINFO}";
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public WebTextbooksViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.Items).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedWebTextbookItem, (MWebTextbook v) => v != null).ToPropertyEx(this, x => x.HasSelectedWebTextbookItem);
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                ItemsAll = new ObservableCollection<MWebTextbook>(await WebTextbookDS.GetDataByLang(vmSettings.SelectedLang.ID));
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        void ApplyFilters()
        {
            Items = NoFilter ? ItemsAll : new ObservableCollection<MWebTextbook>(ItemsAll.Where(o =>
                 (WebTextbookFilter == 0 || o.TEXTBOOKID == WebTextbookFilter)
            ));
            this.RaisePropertyChanged(nameof(Items));
        }
    }
}
