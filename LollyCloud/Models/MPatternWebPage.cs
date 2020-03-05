using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MPatternWebPages
    {
        public List<MPatternWebPage> records { get; set; }
    }
    public class MPatternWebPage : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
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
        public int SEQNUM { get; set; }
        [Reactive]
        [JsonProperty]
        public string WEBPAGE { get; set; }

    }
}
