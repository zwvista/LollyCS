using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class UserDataStore : LollyDataStore<MUser>
    {
        public async Task<List<MUser>> GetData(string username, string password) =>
            (await GetDataByUrl<MUsers>($"USERS?filter=USERNAME,eq,{username}&filter=PASSWORD,eq,{password}")).Records;
    }
}
