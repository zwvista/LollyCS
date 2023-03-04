using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class UnitBlogDataStore : LollyDataStore<MUnitBlog>
    {
        public async Task<MUnitBlog> GetDataByTextbook(int textbookid, int unit) =>
            (await GetDataByUrl<MUnitBlogs>($"UNITBLOGS?filter=TEXTBOOKID,eq,{textbookid}&filter=UNIT,eq,{unit}")).Records.FirstOrDefault();
        private async Task<int> Create(MUnitBlog item) =>
            await CreateByUrl($"UNITBLOGS", item);
        private async Task Update(MUnitBlog item) =>
            Debug.WriteLine(await UpdateByUrl($"UNITBLOGS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Update(int textbookid, int unit, string content)
        {
            var item = (await GetDataByTextbook(textbookid, unit)) ?? new MUnitBlog
            {
                TEXTBOOKID = textbookid,
                UNIT = unit,
            };
            item.CONTENT = content;
            if (item.ID == 0)
                await Create(item);
            else
                await Update(item);
        }
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"UNITBLOGS/{id}"));
    }
}
