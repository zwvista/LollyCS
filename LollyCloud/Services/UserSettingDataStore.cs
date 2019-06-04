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

        public async Task<bool> UpdateLang(int id, int langid) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE1={langid}");

        public async Task<bool> UpdateTextbook(int id, int textbookid) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE1={textbookid}");

        public async Task<bool> UpdateDictItem(int id, string dictitem) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE2={dictitem}");

        public async Task<bool> UpdateDictNote(int id, int dictnoteid) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE3={dictnoteid}");

        public async Task<bool> UpdateDictTranslation(int id, int dicttranslationid) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE1={dicttranslationid}");

        public async Task<bool> UpdateUnitFrom(int id, int unitfrom) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE1={unitfrom}");

        public async Task<bool> UpdatePartFrom(int id, int partfrom) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE2={partfrom}");

        public async Task<bool> UpdateUnitTo(int id, int unitto) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE3={unitto}");

        public async Task<bool> UpdatePartTo(int id, int partto) =>
        await UpdateByUrl($"USERSETTINGS/{id}", $"VALUE4={partto}");
    }
}
