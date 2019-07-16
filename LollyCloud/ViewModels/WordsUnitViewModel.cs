using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class WordsUnitViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        LangWordDataStore langWordDS = new LangWordDataStore();
        NoteViewModel vmNote;
        MDictNote DictNote => vmNote.DictNote;

        public ObservableCollection<MUnitWord> Items { get; set; }
        string _NewWord = "";
        public string NewWord
        {
            get => _NewWord;
            set => this.RaiseAndSetIfChanged(ref _NewWord, value);
        }

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public static async Task<WordsUnitViewModel> CreateAsync(SettingsViewModel vmSettings, bool inTextbook, bool needCopy)
        {
            var o = new WordsUnitViewModel();
            o.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            o.Items = new ObservableCollection<MUnitWord>(await (inTextbook ? o.unitWordDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                o.unitWordDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks)));
            o.vmNote = new NoteViewModel(o.vmSettings);
            return o;
        }

        public async Task UpdateSeqNum(int id, int seqnum) => await unitWordDS.UpdateSeqNum(id, seqnum);
        public async Task UpdateNote(int wordid, string note) => await langWordDS.UpdateNote(wordid, note);
        public async Task Update(MUnitWord item)
        {
            var wordid = item.WORDID;
            var lstUnit = await unitWordDS.GetDataByLangWord(wordid);
            if (lstUnit.IsEmpty()) return;
            var itemLang = new MLangWord(item);
            var lstLangOld = await langWordDS.GetDataById(wordid);
            if (!lstLangOld.IsEmpty() && lstLangOld[0].WORD == item.WORD)
            {
                await langWordDS.UpdateNote(wordid, item.NOTE);
                return;
            }
            var lstLangNew = await langWordDS.GetDataByLangWord(item.LANGID, item.WORD);
            async Task f()
            {
                itemLang = lstLangNew[0];
                wordid = itemLang.ID;
                var b = itemLang.CombineNote(item.NOTE);
                item.NOTE = itemLang.NOTE;
                if (b) await langWordDS.UpdateNote(wordid, item.NOTE);
            }
            if (lstUnit.Count == 1)
                if (lstLangNew.IsEmpty())
                    await langWordDS.Update(itemLang);
                else
                {
                    await langWordDS.Delete(wordid);
                    await f();
                }
            else if (lstLangNew.IsEmpty())
            {
                itemLang.ID = 0;
                await langWordDS.Create(itemLang);
            }
            else
                await f();
            item.WORDID = wordid;
            await unitWordDS.Update(item);
        }
        public async Task<int> Create(MUnitWord item)
        {
            var lstLang = await langWordDS.GetDataByLangWord(item.LANGID, item.WORD);
            int wordid;
            if (lstLang.IsEmpty())
            {
                var itemLang = new MLangWord(item);
                wordid = await langWordDS.Create(itemLang);
            }
            else
            {
                var itemLang = lstLang[0];
                wordid = itemLang.ID;
                var b = itemLang.CombineNote(item.NOTE);
                item.NOTE = itemLang.NOTE;
                if (b) await langWordDS.UpdateNote(wordid, item.NOTE);
            }
            item.WORDID = wordid;
            return await unitWordDS.Create(item);
        }
        public async Task Delete(MUnitWord item)
        {
            await unitWordDS.Delete(item.ID);
            var lst = await unitWordDS.GetDataByLangWord(item.WORDID);
            if (lst.IsEmpty())
                await langWordDS.Delete(item.WORDID);
        }

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= Items.Count; i++)
            {
                var item = Items[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }

        public MUnitWord NewUnitWord()
        {
            var maxElem = Items.MaxBy(o => (o.UNIT, o.PART, o.SEQNUM)).FirstOrDefault();
            return new MUnitWord
            {
                LANGID = vmSettings.SelectedLang.ID,
                TEXTBOOKID = maxElem?.UNIT ?? vmSettings.USUNITTO,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
        }

        public async Task GetNote(int index)
        {
            var item = Items[index];
            var note = await vmNote.GetNote(item.WORD);
            item.NOTE = note;
            await Update(item);
        }
        public async Task ClearNote(int index)
        {
            var item = Items[index];
            item.NOTE = NoteViewModel.ZeroNote;
            await Update(item);
        }

        public async Task GetNotes(bool ifEmpty, Action<int> oneComplete, Action allComplete) =>
            await vmNote.GetNotes(Items.Count, i => !ifEmpty || string.IsNullOrEmpty(Items[i].NOTE),
                async i =>
                {
                    await GetNote(i);
                    oneComplete(i);
                }, allComplete);
        public async Task ClearNotes(bool ifEmpty, Action<int> oneComplete, Action allComplete) =>
            await vmNote.GetNotes(Items.Count, i => !ifEmpty || string.IsNullOrEmpty(Items[i].NOTE),
                async i =>
                {
                    await ClearNote(i);
                    oneComplete(i);
                }, allComplete);
    }
}
