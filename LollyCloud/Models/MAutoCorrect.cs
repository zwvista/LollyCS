using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MAutoCorrects
    {
        public List<MAutoCorrect> records { get; set; }
    }

    public class MAutoCorrect : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LANGID { get; set; }
        [Reactive]
        [JsonProperty]
        public int SEQNUM { get; set; }
        [Reactive]
        [JsonProperty]
        public string INPUT { get; set; }
        [Reactive]
        [JsonProperty]
        public string EXTENDED { get; set; }
        [Reactive]
        [JsonProperty]
        public string BASIC { get; set; }

        public static string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrects,
                                  Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
        lstAutoCorrects.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }

}
