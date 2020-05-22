using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyCloud
{
    public class DictionaryDataStore : LollyDataStore<MDictionary>
    {
        public async Task<List<MDictionary>> GetDictsByLang(int langid) =>
        (await GetDataByUrl<MDictsReference>($"VDICTIONARIES?filter=LANGIDFROM,eq,{langid}&order=SEQNUM&order=DICTNAME")).Records;
        public async Task<List<MDictionary>> GetDictsReferenceByLang(int langid) =>
        (await GetDataByUrl<MDictsReference>($"VDICTSREFERENCE?filter=LANGIDFROM,eq,{langid}&order=SEQNUM&order=DICTNAME")).Records;
        public async Task<List<MDictionary>> GetDictsNoteByLang(int langid) =>
        (await GetDataByUrl<MDictsNote>($"VDICTSNOTE?filter=LANGIDFROM,eq,{langid}")).Records;
        public async Task<List<MDictionary>> GetDictsTranslationByLang(int langid) =>
        (await GetDataByUrl<MDictsTranslation>($"VDICTSTRANSLATION?filter=LANGIDFROM,eq,{langid}")).Records;
        public async Task<int> Create(MDictionary item) =>
        await CreateByUrl($"DICTIONARIES", item);
        public async Task Update(MDictionary item) =>
        Debug.WriteLine(await UpdateByUrl($"DICTIONARIES/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"DICTIONARIES/{id}"));
    }
}
