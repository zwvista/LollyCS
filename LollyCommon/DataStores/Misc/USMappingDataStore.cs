using System.Collections.Generic;
using System.Threading.Tasks;


namespace LollyCommon
{
    public class USMappingDataStore : LollyDataStore<MUSMapping>
    {
        public async Task<List<MUSMapping>> GetData() =>
            (await GetDataByUrl<MUSMappings>("USMAPPINGS")).Records;
    }
}
