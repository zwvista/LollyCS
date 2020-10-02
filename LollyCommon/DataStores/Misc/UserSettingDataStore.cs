using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class UserSettingDataStore : LollyDataStore<MUserSetting>
    {
        public async Task<List<MUserSetting>> GetDataByUser(int userid) =>
        (await GetDataByUrl<MUserSettings>($"USERSETTINGS?filter=USERID,eq,{userid}")).Records;

        public async Task Update(MUserSettingInfo info, int v) =>
        await Update(info, v.ToString());

        public async Task Update(MUserSettingInfo info, string v) =>
        Debug.WriteLine(await UpdateByUrl($"USERSETTINGS/{info.USERSETTINGID}", $"VALUE{info.VALUEID}={v}"));
    }
}
