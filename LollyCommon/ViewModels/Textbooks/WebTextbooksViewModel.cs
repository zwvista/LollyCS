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
        public SettingsViewModel vmSettings;
        WebTextbookDataStore WebTextbookDS = new WebTextbookDataStore();
        [Reactive]
        public MWebTextbook SelectedWebTextbookItem { get; set; }
        public bool HasSelectedWebTextbookItem { [ObservableAsProperty] get; }

        public ObservableCollection<MWebTextbook> Items { get; set; } = new ObservableCollection<MWebTextbook>();
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
                Items = new ObservableCollection<MWebTextbook>(lst);
                this.RaisePropertyChanged(nameof(Items));
            });

    }
}
