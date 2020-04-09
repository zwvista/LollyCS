using ReactiveUI;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MDictionary : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int DICTID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGIDFROM { get; set; }
        [JsonProperty]
        [Reactive]
        public string LANGNAMEFROM { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGIDTO { get; set; }
        [JsonProperty]
        [Reactive]
        public string LANGNAMETO { get; set; }
        [JsonProperty]
        [Reactive]
        public int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public int DICTTYPEID { get; set; }
        [JsonProperty]
        [Reactive]
        public string DICTTYPENAME { get; set; }
        [JsonProperty]
        [Reactive]
        public string DICTNAME { get; set; }
        [JsonProperty]
        [Reactive]
        public string URL { get; set; }
        [JsonProperty]
        [Reactive]
        public string CHCONV { get; set; }
        [JsonProperty]
        [Reactive]
        public string AUTOMATION { get; set; }
        [JsonProperty]
        [Reactive]
        public string TRANSFORM { get; set; }
        [JsonProperty]
        [Reactive]
        public int? WAIT { get; set; }
        [JsonProperty]
        [Reactive]
        public string TEMPLATE { get; set; }
        [JsonProperty]
        [Reactive]
        public string TEMPLATE2 { get; set; }

        public string UrlString(string word, List<MAutoCorrect> lstAutoCorrects)
        {
            var word2 = CHCONV == "BASIC" ? MAutoCorrect.AutoCorrect(word, lstAutoCorrects, o => o.EXTENDED, o => o.BASIC) : word;
            var url = URL.Replace("{0}", HttpUtility.UrlEncode(word2));
            return url;
        }

        public string HtmlString(string html, string word, bool useTemplate2 = false)
        {
            var template = useTemplate2 && !string.IsNullOrEmpty(TEMPLATE2) ? TEMPLATE2 : TEMPLATE;
            return CommonApi.ExtractTextFromHtml(html, TRANSFORM, template, (text, template2) =>
                string.Format(template2, word, CommonApi.CssFolder, text));
        }
    }

    public class MDictsReference
    {
        public List<MDictionary> records { get; set; }
    }

    public class MDictsNote
    {
        public List<MDictionary> records { get; set; }
    }

    public class MDictsTranslation
    {
        public List<MDictionary> records { get; set; }
    }

}
