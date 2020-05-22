using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;
using System.Web;

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
    public class MDictionaryEdit : ReactiveValidationObject<MDictionaryEdit>
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public string DICTNAME { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MDictionaryEdit()
        {
            this.ValidationRule(x => x.DICTNAME, v => !string.IsNullOrWhiteSpace(v), "DICTNAME must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }

    public class MDictsReference
    {
        [JsonProperty("records")]
        public List<MDictionary> Records { get; set; }
    }

    public class MDictsNote
    {
        [JsonProperty("records")]
        public List<MDictionary> Records { get; set; }
    }

    public class MDictsTranslation
    {
        [JsonProperty("records")]
        public List<MDictionary> Records { get; set; }
    }

}
