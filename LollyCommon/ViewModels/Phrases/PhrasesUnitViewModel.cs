using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PhrasesUnitViewModel : PhrasesBaseViewModel
    {
        bool inTextbook;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();

        public ObservableCollection<MUnitPhrase> PhraseItems { get; set; } = [];
        public bool NoFilter => string.IsNullOrEmpty(TextFilter) && TextbookFilter == 0;
        public string StatusText => $"{PhraseItems.Count} Phrases in {(inTextbook ? vmSettings.UNITINFO : vmSettings.LANGINFO)}";

        public PhrasesUnitViewModel(SettingsViewModel vmSettings, bool inTextbook, bool needCopy, bool paged) : base(vmSettings, needCopy, paged)
        {
            this.inTextbook = inTextbook;
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PhraseItems = new ObservableCollection<MUnitPhrase>(inTextbook ? await unitPhraseDS.GetDataByTextbookUnitPart(
                    vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO, TextFilter, ScopeFilter) :
                    await unitPhraseDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks, TextFilter, ScopeFilter, TextbookFilter,
                    Paged ? PageNo : null, Paged ? PageSize : null));
                this.RaisePropertyChanged(nameof(PhraseItems));
                IsBusy = false;
            });
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter, x => x.TextbookFilter).Subscribe(_ => Reload());
            this.WhenAnyValue(x => x.PhraseItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();

        public async Task UpdateSeqNum(int id, int seqnum) => await unitPhraseDS.UpdateSeqNum(id, seqnum);
        public async Task Update(MUnitPhrase item)
        {
            await unitPhraseDS.Update(item);
            var o = await unitPhraseDS.GetDataById(item.ID, vmSettings.Textbooks);
            o?.CopyProperties(item);
        }
        public async Task Create(MUnitPhrase item)
        {
            int id = await unitPhraseDS.Create(item);
            var o = await unitPhraseDS.GetDataById(id, vmSettings.Textbooks);
            o?.CopyProperties(item);
            PhraseItems.Add(o);
        }
        public async Task Delete(MUnitPhrase item) =>
            await unitPhraseDS.Delete(item);

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= PhraseItems.Count; i++)
            {
                var item = PhraseItems[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public MUnitPhrase NewUnitPhrase()
        {
            var maxElem = PhraseItems.IsEmpty() ? null : PhraseItems.MaxByWithTies(o => (o.UNIT, o.PART, o.SEQNUM)).First();
            return new MUnitPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
                TEXTBOOKID = vmSettings.USTEXTBOOK,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
        }
    }
}
