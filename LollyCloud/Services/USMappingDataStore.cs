using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LollyShared
{
    public class USMappingDataStore : LollyDataStore<MUSMapping>
    {
        public async Task<List<MUSMapping>> GetData() =>
        (await GetDataByUrl<MUSMappings>("USMAPPINGS")).records;
    }
}
