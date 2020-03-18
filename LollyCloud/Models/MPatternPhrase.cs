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
    [JsonObject(MemberSerialization.OptOut)]
    public class MPatternPhrase : ReactiveObject
    {
        [Reactive]
        public int PATTERNID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PATTERN { get; set; }
        [Reactive]
        public string NOTE { get; set; }
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public int PHRASEID { get; set; }
        [Reactive]
        public string PHRASE { get; set; }
        [Reactive]
        public string TRANSLATION { get; set; }

    }
}
