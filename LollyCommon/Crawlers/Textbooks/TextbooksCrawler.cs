using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon
{
    public abstract class TextbooksCrawler
    {
        protected const string delim = "@@@@";
        protected HttpClient client = new HttpClient();
        public abstract Task Step1();
        public abstract Task Step2();
        protected async Task Step2(Func<string[], (MWebPage, MWebTextbook)> f)
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storewt = new WebTextbookDataStore();
            int i = 1;
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                var (wp, wt) = f(a);
                var wpid = await storewp.Create(wp);
                if (wpid == 0) continue;
                wt.WEBPAGEID = wpid;
                wt.UNIT = i++;
                await storewt.Create(wt);
            }
        }
        protected async Task Step2(int textbookid) =>
            await Step2(a =>
            {
                string url = a[0], title = a[1];
                var wp = new MWebPage
                {
                    TITLE = title,
                    URL = url,
                };
                var wt = new MWebTextbook
                {
                    TEXTBOOKID = textbookid,
                };
                return (wp, wt);
            });
    }
}
