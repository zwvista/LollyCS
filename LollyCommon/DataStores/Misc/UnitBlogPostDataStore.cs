using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class UnitBlogPostDataStore : LollyDataStore<MUnitBlogPost>
    {
        public async Task<MUnitBlogPost> GetDataByTextbook(int textbookid, int unit) =>
            (await GetDataByUrl<MUnitBlogPosts>($"UNITBLOGPOSTS?filter=TEXTBOOKID,eq,{textbookid}&filter=UNIT,eq,{unit}")).Records.FirstOrDefault();
        private async Task<int> Create(MUnitBlogPost item) =>
            await CreateByUrl($"UNITBLOGPOSTS", item);
        private async Task Update(MUnitBlogPost item) =>
            Debug.WriteLine(await UpdateByUrl($"UNITBLOGPOSTS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Update(int textbookid, int unit, string content)
        {
            var item = (await GetDataByTextbook(textbookid, unit)) ?? new MUnitBlogPost
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
            Debug.WriteLine(await DeleteByUrl($"UNITBLOGPOSTS/{id}"));
    }
}
