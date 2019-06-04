using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class DictReferenceDataStore : LollyDataStore<MDictReference>
    {
        public async Task<List<MDictReference>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MDictsReference>($"VDICTSREFERENCE?filter=LANGIDFROM,eq,{langid}")).records;
    }
    public class DictNoteDataStore : LollyDataStore<MDictNote>
    {
        public async Task<List<MDictNote>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MDictsNote>($"VDICTSNOTE?filter=LANGIDFROM,eq,{langid}")).records;
    }
    public class DictTranslationDataStore : LollyDataStore<MDictTranslation>
    {
        public async Task<List<MDictTranslation>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MDictsTranslation>($"VDICTSTRANSLATION?filter=LANGIDFROM,eq,{langid}")).records;
    }
}
