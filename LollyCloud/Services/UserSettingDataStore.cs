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
    public class UserSettingDataStore : LollyDataStore<MUserSetting>
    {
        public async Task<List<MUserSetting>> GetDataByUser(int userid) =>
        (await GetDataByUrl<MUserSettings>($"USERSETTINGS?filter=USERID,eq,{userid}")).records;

        public async Task<bool> Update(MUserSettingInfo info, int v) =>
        await Update(info, v.ToString());

        public async Task<bool> Update(MUserSettingInfo info, string v) =>
        await UpdateByUrl($"USERSETTINGS/{info.USERSETTINGID}", $"VALUE{info.VALUEID}={v}");
    }
}
