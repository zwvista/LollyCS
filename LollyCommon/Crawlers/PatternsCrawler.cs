using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers
{
    public abstract class PatternsCrawler
    {
        protected const string delim = "@@@@";
        protected HttpClient client = new HttpClient();
        public abstract Task Step1();
        public abstract Task Step2();
        protected async Task Step2(Func<string[], (MPattern, MWebPage)> f)
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storept = new PatternDataStore();
            var storeptwp = new PatternWebPageDataStore();
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                var (pt, wp) = f(a);
                var wpid = await storewp.Create(wp);
                if (wpid == 0) continue;
                var ptid = await storept.Create(pt);
                var ptwp = new MPatternWebPage
                {
                    PATTERNID = ptid,
                    WEBPAGEID = wpid,
                    SEQNUM = 1,
                };
                await storeptwp.Create(ptwp);
            }
        }
        protected async Task Step2(int langid, string tags) =>
            await Step2(a =>
            {
                string url = a[0], title = a[1];
                var pt = new MPattern
                {
                    LANGID = langid,
                    PATTERN = title,
                    TAGS = tags,
                };
                var wp = new MWebPage
                {
                    TITLE = title,
                    URL = url,
                };
                return (pt, wp);
            });
    }
}
