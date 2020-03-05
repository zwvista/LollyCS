using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MUnitWords
    {
        public List<MUnitWord> records { get; set; }
    }
    public class MUnitWord : ReactiveObject, MWordInterface
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LANGID { get; set; }
        [Reactive]
        [JsonProperty]
        public int TEXTBOOKID { get; set; }
        [Reactive]
        [JsonProperty]
        public string TEXTBOOKNAME { get; set; }
        [Reactive]
        [JsonProperty]
        public int UNIT { get; set; }
        [Reactive]
        [JsonProperty]
        public int PART { get; set; }
        [Reactive]
        [JsonProperty]
        public int SEQNUM { get; set; }
        [Reactive]
        [JsonProperty]
        public int WORDID { get; set; }
        [Reactive]
        [JsonProperty]
        public string WORD { get; set; }
        [Reactive]
        [JsonProperty]
        public string NOTE { get; set; }
        [Reactive]
        [JsonProperty]
        public int FAMIID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LEVEL { get; set; }
        [Reactive]
        [JsonProperty]
        public int CORRECT { get; set; }
        [Reactive]
        [JsonProperty]
        public int TOTAL { get; set; }
        [Reactive]
        public bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";
    }
}
