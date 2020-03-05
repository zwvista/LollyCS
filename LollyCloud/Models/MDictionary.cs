using ReactiveUI;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MDictionary : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int DICTID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LANGIDFROM { get; set; }
        [Reactive]
        [JsonProperty]
        public string LANGNAMEFROM { get; set; }
        [Reactive]
        [JsonProperty]
        public int LANGIDTO { get; set; }
        [Reactive]
        [JsonProperty]
        public string LANGNAMETO { get; set; }
        [Reactive]
        [JsonProperty]
        public int SEQNUM { get; set; }
        [Reactive]
        [JsonProperty]
        public int DICTTYPEID { get; set; }
        [Reactive]
        [JsonProperty]
        public string DICTTYPENAME { get; set; }
        [Reactive]
        [JsonProperty]
        public string DICTNAME { get; set; }
        [Reactive]
        [JsonProperty]
        public string URL { get; set; }
        [Reactive]
        [JsonProperty]
        public string CHCONV { get; set; }
        [Reactive]
        [JsonProperty]
        public string AUTOMATION { get; set; }
        [Reactive]
        [JsonProperty]
        public string TRANSFORM_WIN { get; set; }
        [Reactive]
        [JsonProperty]
        public string TRANSFORM { get; set; }
        [Reactive]
        [JsonProperty]
        public int? WAIT { get; set; }
        [Reactive]
        [JsonProperty]
        public string TEMPLATE { get; set; }
        [Reactive]
        [JsonProperty]
        public string TEMPLATE2 { get; set; }

        public string UrlString(string word, List<MAutoCorrect> lstAutoCorrects)
        {
            var word2 = CHCONV == "BASIC" ? MAutoCorrect.AutoCorrect(word, lstAutoCorrects, o => o.EXTENDED, o => o.BASIC) : word;
            var url = URL.Replace("{0}", HttpUtility.UrlEncode(word2));
            return url;
        }
    }

    public class MDictsReference
    {
        public List<MDictReference> records { get; set; }
    }
    public class MDictReference : MDictionary
    {
        public string HtmlString(string html, string word, bool useTransformWin = false, bool useTemplate2 = false)
        {
            var template = useTemplate2 && !string.IsNullOrEmpty(TEMPLATE2) ? TEMPLATE2 : TEMPLATE;
            var transform = useTransformWin ? TRANSFORM_WIN : TRANSFORM;
            return CommonApi.ExtractTextFromHtml(html, transform, template, (text, template2) =>
                string.Format(template2, word, CommonApi.CssFolder, text));
        }
    }

    public class MDictItem : ReactiveObject
    {
        [Reactive]
        public string DICTID { get; set; }
        [Reactive]
        public string DICTNAME { get; set; }
        public MDictItem(string id, string name)
        {
            DICTID = id;
            DICTNAME = name;
        }
        public string[] DictIDs => DICTID.Split(',');
    }

    public class MDictsNote
    {
        public List<MDictNote> records { get; set; }
    }
    public class MDictNote : MDictionary{ }

    public class MDictsTranslation
    {
        public List<MDictTranslation> records { get; set; }
    }
    public class MDictTranslation : MDictionary { }

}
