using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MUnitPhrases
    {
        public List<MUnitPhrase> records { get; set; }
    }
    public class MUnitPhrase : ReactiveObject, MPhraseInterface
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
        public int PHRASEID { get; set; }
        [Reactive]
        [JsonProperty]
        public string PHRASE { get; set; }
        [Reactive]
        [JsonProperty]
        public string TRANSLATION { get; set; }
        [Reactive]
        public bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);
    }
}
