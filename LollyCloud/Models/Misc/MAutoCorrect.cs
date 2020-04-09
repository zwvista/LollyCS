using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MAutoCorrects
    {
        public List<MAutoCorrect> records { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MAutoCorrect : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public string INPUT { get; set; }
        [JsonProperty]
        [Reactive]
        public string EXTENDED { get; set; }
        [JsonProperty]
        [Reactive]
        public string BASIC { get; set; }

        public static string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrects,
                                  Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
        lstAutoCorrects.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }

}
