using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace LollyCommon
{
    public class LangBlogGroupDataStore : LollyDataStore<MLangBlogGroup>
    {
        public async Task<List<MLangBlogGroup>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MLangBlogGroups>($"LANGBLOGGROUPS?filter=LANGID,eq,{langid}&order=NAME")).Records;
        public async Task<List<MLangBlogGroup>> GetDataByLangPost(int langid, int postid) =>
            (await GetDataByUrl<MLangBlogGPs>($"VLANGBLOGGP?filter=LANGID,eq,{langid}&filter=POSTID,eq,{postid}&order=GROUPNAME")).Records
            .Select(o => new MLangBlogGroup
            {
                ID = o.GROUPID,
                LANGID = langid,
                GROUPNAME = o.GROUPNAME,
                GPID = o.ID,
            }).Distinct(o => o.ID).ToList();
        public async Task<int> Create(MLangBlogGroup item) =>
            await CreateByUrl($"LANGBLOGGROUPS", item);
        public async Task Update(MLangBlogGroup item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGBLOGGROUPS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"LANGBLOGGROUPS/{id}"));
    }
}
