using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCommon
{
    public class MPatternPhrases
    {
        [JsonProperty("records")]
        public List<MPatternPhrase> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MPatternPhrase : ReactiveObject
    {
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
        public string NOTE { get; set; }
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public int PHRASEID { get; set; }
        [JsonProperty]
        [Reactive]
        public string PHRASE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string TRANSLATION { get; set; } = "";

        public MPatternPhrase()
        {
        }

    }
}
