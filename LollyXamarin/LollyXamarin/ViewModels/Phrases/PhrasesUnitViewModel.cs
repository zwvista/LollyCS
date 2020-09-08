using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class PhrasesUnitViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        bool inTextbook;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();

        List<MUnitPhrase> PhraseItemsAll { get; set; } = new List<MUnitPhrase>();
        public ObservableCollection<MUnitPhrase> PhraseItems { get; set; } = new ObservableCollection<MUnitPhrase>();
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public string ScopeFilter { get; set; } = SettingsViewModel.ScopePhraseFilters[0];
        [Reactive]
        public int TextbookFilter { get; set; }
        bool NoFilter => string.IsNullOrEmpty(TextFilter) && TextbookFilter == 0;
        public string StatusText => $"{PhraseItems.Count} Phrases in {(inTextbook ? vmSettings.UNITINFO : vmSettings.LANGINFO)}";
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; }

        public PhrasesUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.inTextbook = inTextbook;
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.TextbookFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.PhraseItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            Reload();
        }
        public void Reload() =>
            (inTextbook ? unitPhraseDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                unitPhraseDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks))
            .ToObservable().Subscribe(lst =>
            {
                PhraseItemsAll = lst;
                ApplyFilters();
            });
        void ApplyFilters()
        {
            PhraseItems = new ObservableCollection<MUnitPhrase>(NoFilter ? PhraseItemsAll : PhraseItemsAll.Where(o =>
                (string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Phrase" ? o.PHRASE : o.TRANSLATION ?? "").ToLower().Contains(TextFilter.ToLower())) &&
                (TextbookFilter == 0 || o.TEXTBOOKID == TextbookFilter)
            ));
            this.RaisePropertyChanged(nameof(PhraseItems));
        }

        public async Task UpdateSeqNum(int id, int seqnum) => await unitPhraseDS.UpdateSeqNum(id, seqnum);
        public async Task<MUnitPhrase> Update(MUnitPhrase item)
        {
            await unitPhraseDS.Update(item);
            var o = await unitPhraseDS.GetDataById(item.ID, vmSettings.Textbooks);
            return o;
        }
        public async Task<MUnitPhrase> Create(MUnitPhrase item)
        {
            int id = await unitPhraseDS.Create(item);
            var o = await unitPhraseDS.GetDataById(id, vmSettings.Textbooks);
            return o;
        }
        public void Add(MUnitPhrase item)
        {
            PhraseItemsAll.Add(item);
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
        public void Replace(int index, MUnitPhrase item)
        {
            PhraseItems[index] = item;
            this.RaisePropertyChanged(nameof(PhraseItems));
        }
        public async Task Delete(MUnitPhrase item) =>
            await unitPhraseDS.Delete(item);

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= PhraseItemsAll.Count; i++)
            {
                var item = PhraseItemsAll[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public MUnitPhrase NewUnitPhrase()
        {
            var maxElem = PhraseItemsAll.IsEmpty() ? null : PhraseItemsAll.MaxBy(o => (o.UNIT, o.PART, o.SEQNUM)).First();
            return new MUnitPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
                TEXTBOOKID = vmSettings.USTEXTBOOKID,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
        }
    }
}
