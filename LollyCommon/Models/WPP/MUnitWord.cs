using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class MUnitWords
    {
        [JsonProperty("records")]
        public List<MUnitWord> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MUnitWord : ReactiveObject, MWordInterface
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public int TEXTBOOKID { get; set; }
        [JsonProperty]
        [Reactive]
        public string TEXTBOOKNAME { get; set; }
        [JsonProperty]
        [Reactive]
        public int UNIT { get; set; }
        [JsonProperty]
        [Reactive]
        public int PART { get; set; }
        [JsonProperty]
        [Reactive]
        public int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public int WORDID { get; set; }
        [JsonProperty]
        [Reactive]
        public string WORD { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string NOTE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public int FAMIID { get; set; }
        [JsonProperty]
        [Reactive]
        public int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public int TOTAL { get; set; }
        [Reactive]
        public bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";

        public MUnitWord()
        {
        }
    }
    public class MUnitWordEdit : ReactiveValidationObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public string TEXTBOOKNAME { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public int WORDID { get; set; }
        [Reactive]
        public string WORD { get; set; } = "";
        [Reactive]
        public string NOTE { get; set; }
        [Reactive]
        public int FAMIID { get; set; }
        public MTextbook Textbook { get; set; }
        [Reactive]
        public string ACCURACY { get; set; }
        [Reactive]
        public string WORDS { get; set; } = "";
        public MSelectItem UNITItem
        {
            get => Textbook.Units.SingleOrDefault(o => o.Value == UNIT);
            set { if (value != null) UNIT = value.Value; }
        }
        public MSelectItem PARTItem
        {
            get => Textbook.Parts.SingleOrDefault(o => o.Value == PART);
            set { if (value != null) PART = value.Value; }
        }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MUnitWordEdit()
        {
            this.ValidationRule(x => x.WORD, v => !string.IsNullOrWhiteSpace(v), "WORD must not be empty");
        }
    }
}
