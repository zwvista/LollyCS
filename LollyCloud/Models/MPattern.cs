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
    [JsonObject(MemberSerialization.OptOut)]
    public class MPattern : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        public int PATTERNID => ID;
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PATTERN { get; set; }
        [Reactive]
        public string NOTE { get; set; }

    }
}
