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
    public class OnlineTextbooksViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; set; }
        OnlineTextbookDataStore OnlineTextbookDS = new OnlineTextbookDataStore();
        [Reactive]
        public int OnlineTextbookFilter { get; set; }
        public MSelectItem OnlineTextbookFilterItem
        {
            get => vmSettings.OnlineTextbookFilters.SingleOrDefault(o => o.Value == OnlineTextbookFilter);
            set { if (value != null) OnlineTextbookFilter = value.Value; }
        }
        [Reactive]
        public MOnlineTextbook SelectedOnlineTextbookItem { get; set; }
        public bool HasSelectedOnlineTextbookItem { [ObservableAsProperty] get; }

        public ObservableCollection<MOnlineTextbook> ItemsAll { get; set; } = new ObservableCollection<MOnlineTextbook>();
        public ObservableCollection<MOnlineTextbook> Items { get; set; } = new ObservableCollection<MOnlineTextbook>();
        public bool NoFilter => OnlineTextbookFilter == 0;
        public string StatusText => $"{Items.Count} Online Textbooks in {vmSettings.LANGINFO}";
        [Reactive]
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public OnlineTextbooksViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.Items).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedOnlineTextbookItem, (MOnlineTextbook v) => v != null).ToPropertyEx(this, x => x.HasSelectedOnlineTextbookItem);
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                ItemsAll = new ObservableCollection<MOnlineTextbook>(await OnlineTextbookDS.GetDataByLang(vmSettings.SelectedLang.ID));
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        void ApplyFilters()
        {
            Items = NoFilter ? ItemsAll : new ObservableCollection<MOnlineTextbook>(ItemsAll.Where(o =>
                 (OnlineTextbookFilter == 0 || o.TEXTBOOKID == OnlineTextbookFilter)
            ));
            this.RaisePropertyChanged(nameof(Items));
        }
    }
}
