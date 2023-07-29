using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace LollyCommon
{
    public class LangBlogPostDataStore : LollyDataStore<MLangBlogPost>
    {
        public async Task<List<MLangBlogPost>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MLangBlogPosts>($"LANGBLOGPOSTS?filter=LANGID,eq,{langid}&order=TITLE")).Records;
        public async Task<List<MLangBlogPost>> GetDataByLangGroup(int langid, int groupid) =>
            (await GetDataByUrl<MLangBlogGPs>($"VLANGBLOGGP?filter=LANGID,eq,{langid}&filter=GROUPID,eq,{groupid}&order=TITLE")).Records
            .Select(o => new MLangBlogPost
            {
                ID = o.POSTID,
                LANGID = langid,
                TITLE = o.TITLE,
                URL = o.URL,
                GPID = o.ID,
            }).Distinct(o => o.ID).ToList();
        public async Task<int> Create(MLangBlogPost item) =>
            await CreateByUrl($"LANGBLOGPOSTS", item);
        public async Task Update(MLangBlogPost item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGBLOGPOSTS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"LANGBLOGPOSTS/{id}"));
    }
}
