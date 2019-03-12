using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace LollyShared
{
    public class WordsUnitViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        private UnitWordDataStore unitWordDS = new UnitWordDataStore();
        private LangWordDataStore langWordDS = new LangWordDataStore();

        public ObservableCollection<MUnitWord> UnitWords { get; set; }

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public static async Task<WordsUnitViewModel> CreateAsync(SettingsViewModel vmSettings)
        {
            var o = new WordsUnitViewModel();
            o.vmSettings = vmSettings;
            o.UnitWords = new ObservableCollection<MUnitWord>(await o.unitWordDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO));
            return o;
        }

        public async Task<bool> UpdateSeqNum(int id, int seqnum) => await unitWordDS.UpdateSeqNum(id, seqnum);
        public async Task<bool> UpdateNote(int wordid, string note) => await langWordDS.UpdateNote(wordid, note);
        public async Task<bool> Update(MUnitWord item)
        {
            var wordid = item.WORDID;
            var lstUnit = await unitWordDS.GetDataByLangWord(wordid);
            if (lstUnit.IsEmpty()) return true;
            var itemLang = new MLangWord(item);
            var lstLangOld = await langWordDS.GetDataById(wordid);
            if (!lstLangOld.IsEmpty() && lstLangOld[0].WORD == item.WORD)
                return await langWordDS.UpdateNote(wordid, item.NOTE);
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
            return await unitWordDS.Update(item);
        }
    }
}
