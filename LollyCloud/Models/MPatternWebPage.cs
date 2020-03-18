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
    [JsonObject(MemberSerialization.OptOut)]
    public class MPatternWebPage : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int PATTERNID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PATTERN { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public string WEBPAGE { get; set; }

    }
}
