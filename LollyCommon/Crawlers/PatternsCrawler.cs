using System;
using System.Collections.Generic;
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
    }
}
