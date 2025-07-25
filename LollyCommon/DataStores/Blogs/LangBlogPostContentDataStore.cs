﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class LangBlogPostContentDataStore : LollyDataStore<MLangBlogPostContent>
    {
        public async Task<MLangBlogPostContent?> GetDataById(int id) =>
            (await GetDataByUrl<MLangBlogPostContents>($"LANGBLOGPOSTS?filter=ID,eq,{id}"))?.Records.FirstOrDefault();

        public async Task Update(MLangBlogPostContent item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGBLOGPOSTS/{item.ID}", JsonConvert.SerializeObject(item)));
    }
}
