using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MPatterns
    {
        public List<MPattern> records { get; set; }
    }
    public class MPattern : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        public int PATTERNID => ID;
        [Reactive]
        [JsonProperty]
        public int LANGID { get; set; }
        [Reactive]
        [JsonProperty]
        public string PATTERN { get; set; }
        [Reactive]
        [JsonProperty]
        public string NOTE { get; set; }

    }
}
