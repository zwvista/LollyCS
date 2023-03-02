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
        protected async Task Step2(Func<string[], MWebTextbook> f)
        {
            var lines = File.ReadAllLines("b.txt");
            var storewt = new WebTextbookDataStore();
            int i = 1;
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                var wt = f(a);
                wt.UNIT = i++;
                await storewt.Create(wt);
            }
        }
        protected async Task Step2(int textbookid) =>
            await Step2(a =>
            {
                string url = a[0], title = a[1];
                return new MWebTextbook
                {
                    TEXTBOOKID = textbookid,
                    TITLE = title,
                    URL = url,
                };
            });
    }
}
