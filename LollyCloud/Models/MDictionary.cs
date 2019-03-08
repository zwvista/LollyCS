using System;
using System.Collections.Generic;
using System.Web;

namespace LollyShared
{
    public class MDictionary
    {
        public int ID { get; set; }
        public int DICTID { get; set; }
        public int? LANGIDFROM { get; set; }
        public string DICTTYPENAME { get; set; }
        public string DICTNAME { get; set; }
        public string URL { get; set; }
        public string CHCONV { get; set; }
        public string TRANSFORM { get; set; }
        public int? WAIT { get; set; }
        public string TEMPLATE { get; set; }
        public string TEMPLATE2 { get; set; }

        public string UrlString(string word, List<MAutoCorrect> lstAutoCorrects)
        {
            var word2 = CHCONV == "BASIC" ? MAutoCorrect.AutoCorrect(word, lstAutoCorrects, o => o.EXTENDED, o => o.BASIC) : word;
            var url = URL.Replace("{0}", HttpUtility.UrlEncode(word2));
            return url;
        }
    }

    public class MDictsMean
    {
        public List<MDictMean> VDICTSMEAN { get; set; }
    }
    public class MDictMean : MDictionary
    {
        public string HtmlString(string html, string word, bool useTemplate2 = false)
        {
            var template = useTemplate2 && !string.IsNullOrEmpty(TEMPLATE2) ? TEMPLATE2 : TEMPLATE;
            return HtmlApi.ExtractTextFromHtml(html, TRANSFORM, template, (text, template2) =>
                string.Format(template2, word, HtmlApi.CssFolder, text));
        }
    }

    public class MDictItem
    {
        public string DICTID { get; set; }
        public string DICTNAME { get; set; }
        public MDictItem(string id, string name)
        {
            DICTID = id;
            DICTNAME = name;
        }
        string[] DictIDs => DICTID.Split(',');
    }

    public class MDictsNote
    {
        public List<MDictNote> VDICTSNOTE { get; set; }
    }
    public class MDictNote : MDictionary{ }
}
