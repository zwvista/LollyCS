using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
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
        [JsonProperty("results")]
        public int Count { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MUnitWord : ReactiveObject, MWordInterface
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int TEXTBOOKID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string TEXTBOOKNAME { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int UNIT { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int PART { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int WORDID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string WORD { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string NOTE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int FAMIID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int TOTAL { get; set; }
        [Reactive]
        public partial bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";

        public MUnitWord()
        {
        }
    }
    public partial class MUnitWordEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial string TEXTBOOKNAME { get; set; }
        [Reactive]
        public partial int UNIT { get; set; }
        [Reactive]
        public partial int PART { get; set; }
        [Reactive]
        public partial int SEQNUM { get; set; }
        [Reactive]
        public partial int WORDID { get; set; }
        [Reactive]
        public partial string WORD { get; set; } = "";
        [Reactive]
        public partial string NOTE { get; set; }
        [Reactive]
        public partial int FAMIID { get; set; }
        public MTextbook Textbook { get; set; }
        [Reactive]
        public partial string ACCURACY { get; set; }
        [Reactive]
        public partial string WORDS { get; set; } = "";
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
