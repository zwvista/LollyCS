using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MPatternPhrases
    {
        public List<MPatternPhrase> records { get; set; }
    }
    public class MPatternPhrase : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int PATTERNID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LANGID { get; set; }
        [Reactive]
        [JsonProperty]
        public string PATTERN { get; set; }
        [Reactive]
        [JsonProperty]
        public string NOTE { get; set; }
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int SEQNUM { get; set; }
        [Reactive]
        [JsonProperty]
        public int PHRASEID { get; set; }
        [Reactive]
        [JsonProperty]
        public string PHRASE { get; set; }
        [Reactive]
        [JsonProperty]
        public string TRANSLATION { get; set; }

    }
}
