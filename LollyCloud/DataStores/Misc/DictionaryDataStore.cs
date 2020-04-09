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
        public async Task<List<MDictionary>> GetDictsReferenceByLang(int langid) =>
        (await GetDataByUrl<MDictsReference>($"VDICTSREFERENCE?filter=LANGIDFROM,eq,{langid}&order=SEQNUM&order=DICTNAME")).records;
        public async Task<List<MDictionary>> GetDictsNoteByLang(int langid) =>
        (await GetDataByUrl<MDictsNote>($"VDICTSNOTE?filter=LANGIDFROM,eq,{langid}")).records;
        public async Task<List<MDictionary>> GetDictsTranslationByLang(int langid) =>
        (await GetDataByUrl<MDictsTranslation>($"VDICTSTRANSLATION?filter=LANGIDFROM,eq,{langid}")).records;
    }
}
