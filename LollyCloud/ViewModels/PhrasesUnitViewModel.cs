using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class PhrasesUnitViewModel : LollyViewModel
    {
        SettingsViewModel vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        public ObservableCollection<MUnitPhrase> Items { get; set; }

        public static async Task<PhrasesUnitViewModel> CreateAsync(SettingsViewModel vmSettings, bool inTextbook)
        {
            var o = new PhrasesUnitViewModel();
            o.vmSettings = vmSettings;
            o.Items = new ObservableCollection<MUnitPhrase>(await (inTextbook ? o.unitPhraseDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO) :
                o.unitPhraseDS.GetDataByLang(vmSettings.SelectedLang.ID, vmSettings.Textbooks)));
            return o;
        }

        public async Task<bool> UpdateSeqNum(int id, int seqnum) => await unitPhraseDS.UpdateSeqNum(id, seqnum);
        public async Task<bool> UpdateTranslation(int phraseid, string note) => await langPhraseDS.UpdateTranslation(phraseid, note);
        public async Task<bool> Update(MUnitPhrase item)
        {
            var phraseid = item.PHRASEID;
            var lstUnit = await unitPhraseDS.GetDataByLangPhrase(phraseid);
            if (lstUnit.IsEmpty()) return true;
            var itemLang = new MLangPhrase(item);
            var lstLangOld = await langPhraseDS.GetDataById(phraseid);
            if (!lstLangOld.IsEmpty() && lstLangOld[0].PHRASE == item.PHRASE)
                return await langPhraseDS.UpdateTranslation(phraseid, item.TRANSLATION);
            var lstLangNew = await langPhraseDS.GetDataByLangPhrase(item.LANGID, item.PHRASE);
            async Task f()
            {
                itemLang = lstLangNew[0];
                phraseid = itemLang.ID;
                var b = itemLang.CombineTranslation(item.TRANSLATION);
                item.TRANSLATION = itemLang.TRANSLATION;
                if (b) await langPhraseDS.UpdateTranslation(phraseid, item.TRANSLATION);
            }
            if (lstUnit.Count == 1)
                if (lstLangNew.IsEmpty())
                    await langPhraseDS.Update(itemLang);
                else
                {
                    await langPhraseDS.Delete(phraseid);
                    await f();
                }
            else if (lstLangNew.IsEmpty())
            {
                itemLang.ID = 0;
                await langPhraseDS.Create(itemLang);
            }
            else
                await f();
            item.PHRASEID = phraseid;
            return await unitPhraseDS.Update(item);
        }
        public async Task<int> Create(MUnitPhrase item)
        {
            var lstLang = await langPhraseDS.GetDataByLangPhrase(item.LANGID, item.PHRASE);
            int phraseid;
            if (lstLang.IsEmpty())
            {
                var itemLang = new MLangPhrase(item);
                phraseid = await langPhraseDS.Create(itemLang);
            }
            else
            {
                var itemLang = lstLang[0];
                phraseid = itemLang.ID;
                var b = itemLang.CombineTranslation(item.TRANSLATION);
                item.TRANSLATION = itemLang.TRANSLATION;
                if (b) await langPhraseDS.UpdateTranslation(phraseid, item.TRANSLATION);
            }
            item.PHRASEID = phraseid;
            return await unitPhraseDS.Create(item);
        }
        public async Task<bool> Delete(MUnitPhrase item)
        {
            await unitPhraseDS.Delete(item.ID);
            var lst = await unitPhraseDS.GetDataByLangPhrase(item.PHRASEID);
            return !lst.IsEmpty() || await langPhraseDS.Delete(item.PHRASEID);
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

        public MUnitPhrase NewUnitPhrase()
        {
            var maxElem = Items.MaxBy(o => (o.UNIT, o.PART, o.SEQNUM)).FirstOrDefault();
            return new MUnitPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
                TEXTBOOKID = maxElem?.UNIT ?? vmSettings.USUNITTO,
                UNIT = maxElem?.UNIT ?? vmSettings.USUNITTO,
                PART = maxElem?.PART ?? vmSettings.USPARTTO,
                SEQNUM = (maxElem?.SEQNUM ?? 0) + 1,
                Textbook = vmSettings.SelectedTextbook,
            };
        }
    }
}
