using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class MAutoCorrects
    {
        [JsonProperty("records")]
        public List<MAutoCorrect> Records { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public partial class MAutoCorrect : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string INPUT { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string EXTENDED { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string BASIC { get; set; } = "";

        public static string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrects,
                                  Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
        lstAutoCorrects.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }

}
