using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;
using System.Web;

namespace LollyCommon
{
    public class MDictionaries
    {
        [JsonProperty("records")]
        public List<MDictionary> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MDictionary : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int DICTID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGIDFROM { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string LANGNAMEFROM { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int LANGIDTO { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string LANGNAMETO { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int DICTTYPECODE { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string DICTTYPENAME { get; set; } = "";
        [JsonProperty("NAME")]
        [Reactive]
        public partial string DICTNAME { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string URL { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string CHCONV { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string AUTOMATION { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string TRANSFORM { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int WAIT { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string TEMPLATE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string TEMPLATE2 { get; set; } = "";

        public string UrlString(string word, List<MAutoCorrect> lstAutoCorrects)
        {
            var word2 = CHCONV == "BASIC" ? MAutoCorrect.AutoCorrect(word, lstAutoCorrects, o => o.EXTENDED, o => o.BASIC) : word;
            var url = URL.Replace("{0}", HttpUtility.UrlEncode(word2));
            return url;
        }

        public string HtmlString(string html, string word, bool useTemplate2 = false)
        {
            var template = useTemplate2 && !string.IsNullOrEmpty(TEMPLATE2) ? TEMPLATE2 : TEMPLATE;
            return HtmlTransformService.ExtractTextFromHtml(html, TRANSFORM, template, (text, template2) =>
                HtmlTransformService.ApplyTemplate(template2, word, text));
        }
    }
    public partial class MDictionaryEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial int DICTID { get; set; }
        [Reactive]
        public partial int LANGIDFROM { get; set; }
        [Reactive]
        public partial string LANGNAMEFROM { get; set; }
        [Reactive]
        public partial int LANGIDTO { get; set; }
        [Reactive]
        public partial string LANGNAMETO { get; set; }
        [Reactive]
        public partial int SEQNUM { get; set; }
        [Reactive]
        public partial int DICTTYPECODE { get; set; }
        [Reactive]
        public partial string DICTTYPENAME { get; set; }
        [Reactive]
        public partial string DICTNAME { get; set; }
        [Reactive]
        public partial string URL { get; set; }
        [Reactive]
        public partial string CHCONV { get; set; }
        [Reactive]
        public partial string AUTOMATION { get; set; }
        [Reactive]
        public partial string TRANSFORM { get; set; }
        [Reactive]
        public partial int? WAIT { get; set; }
        [Reactive]
        public partial string TEMPLATE { get; set; }
        [Reactive]
        public partial string TEMPLATE2 { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MDictionaryEdit()
        {
            this.ValidationRule(x => x.DICTNAME, v => !string.IsNullOrWhiteSpace(v), "DICTNAME must not be empty");
        }
    }
}
