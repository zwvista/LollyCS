using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers.Patterns
{
    public abstract class PatternsCrawler
    {
        protected const string delim = "@@@@";
        protected HttpClient client = new HttpClient();
        public abstract Task Step1();
        public abstract Task Step2();
        protected async Task Step2(string tag, Func<string[], MPattern> f)
        {
            var storept = new PatternDataStore();
            var patterns = await storept.GetDataByTag(tag);
            var lines = File.ReadAllLines("b.txt");
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                var pt = f(a);
                var pt2 = patterns.Find(o => o.URL == pt.URL);
                if (pt2 != null)
                {
                    await storept.Update(pt);
                    patterns.Remove(pt2);
                }
                else
                    await storept.Create(pt);
            }
            foreach (var pt in patterns)
                await storept.Delete(pt.ID);
        }
        protected async Task Step2(int langid, string tags) =>
            await Step2(tags, a =>
            {
                string url = a[0], title = a[1];
                return new MPattern
                {
                    LANGID = langid,
                    PATTERN = title,
                    TAGS = tags,
                    TITLE = title,
                    URL = url,
                };
            });
    }
}
