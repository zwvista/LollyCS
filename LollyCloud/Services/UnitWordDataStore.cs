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
    public class UnitWordDataStore : LollyDataStore<UnitWord>
    {
        public async Task<IEnumerable<UnitWord>> GetDataByTextbookUnitPart(int textbookid, int unitPartFrom, int unitPartTo) =>
        (await GetDataByUrl<UnitWords>($"VUNITWORDS?transform=1&filter[]=TEXTBOOKID,eq,{textbookid}&filter[]=UNITPART,bt,{unitPartFrom},{unitPartTo}&order[]=UNITPART&order[]=SEQNUM")).VUNITWORDS;

        public async Task<bool> Create(UnitWord item) =>
        await CreateByUrl($"UNITWORDS", item);

        public async Task<bool> UpdateSeqNum(int id, int seqnum) =>
        await UpdateByUrl($"UNITWORDS/{id}", $"SEQNUM={seqnum}");

        public async Task<bool> UpdateNote(int id, String note) =>
        await UpdateByUrl($"UNITWORDS/{id}", $"NOTE={note}");

        public async Task<bool> Update(UnitWord item) =>
        await UpdateByUrl($"UNITWORDS/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"UNITWORDS/{id}");
    }
}
