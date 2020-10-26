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
    public class TextbooksViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        TextbookDataStore textbookDS = new TextbookDataStore();
        [Reactive]
        public MTextbook SelectedTextbookItem { get; set; }
        public bool HasSelectedTextbookItem { [ObservableAsProperty] get; }

        public ObservableCollection<MTextbook> Items { get; set; } = new ObservableCollection<MTextbook>();
        public string StatusText => $"{Items.Count} Textbooks in {vmSettings.LANGINFO}";

        public TextbooksViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.WhenAnyValue(x => x.Items).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedTextbookItem, (MTextbook v) => v != null).ToPropertyEx(this, x => x.HasSelectedTextbookItem);
            Reload();
        }
        public void Reload() =>
            textbookDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                Items = new ObservableCollection<MTextbook>(lst);
                this.RaisePropertyChanged(nameof(Items));
            });
        public MTextbook NewTextbook() =>
            new MTextbook
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
        public MTextbook NewTextbookByCopy(MTextbook item)
        {
            var o = new MTextbook();
            item.CopyProperties(o, nameof(MTextbook.ID));
            return o;
        }
            

        public void Add(MTextbook item)
        {
            Items.Add(item);
        }

        public async Task Update(MTextbook item) => await textbookDS.Update(item);
        public async Task Create(MTextbook item) => item.ID = await textbookDS.Create(item);
        public async Task Delete(int id) => await textbookDS.Delete(id);

    }
}
