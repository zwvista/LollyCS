using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCommon
{
    public class MPatternWebPages
    {
        [JsonProperty("records")]
        public List<MPatternWebPage> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MPatternWebPage : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int PATTERNID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public string PATTERN { get; set; }
        [JsonProperty]
        [Reactive]
        public int WEBPAGEID { get; set; }
        [JsonProperty]
        [Reactive]
        public int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string URL { get; set; } = "";

        public MPatternWebPage()
        {
        }
    }
    public class MPatternWebPageEdit : ReactiveValidationObject<MPatternWebPageEdit>
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int PATTERNID { get; set; }
        [Reactive]
        public string PATTERN { get; set; } = "";
        [Reactive]
        public int WEBPAGEID { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public string TITLE { get; set; } = "";
        [Reactive]
        public string URL { get; set; } = "";
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MPatternWebPageEdit()
        {
            this.ValidationRule(x => x.TITLE, v => !string.IsNullOrWhiteSpace(v), "TITLE must not be empty");
            this.ValidationRule(x => x.URL, v => !string.IsNullOrWhiteSpace(v), "URL must not be empty");
        }
    }
}
