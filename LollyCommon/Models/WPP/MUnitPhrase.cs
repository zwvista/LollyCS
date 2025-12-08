using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class MUnitPhrases
    {
        [JsonProperty("records")]
        public List<MUnitPhrase> Records { get; set; }
        [JsonProperty("results")]
        public int Count { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MUnitPhrase : ReactiveObject, MPhraseInterface
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
        public partial int PHRASEID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string PHRASE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string TRANSLATION { get; set; } = "";
        [Reactive]
        public partial bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);

        public MUnitPhrase()
        {
        }
    }
    public partial class MUnitPhraseEdit : ReactiveValidationObject
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
        public partial int PHRASEID { get; set; }
        [Reactive]
        public partial string PHRASE { get; set; } = "";
        [Reactive]
        public partial string TRANSLATION { get; set; }
        public MTextbook Textbook { get; set; }
        [Reactive]
        public partial string PHRASES { get; set; } = "";
        public MSelectItem? UNITItem
        {
            get => Textbook.Units.SingleOrDefault(o => o.Value == UNIT);
            set { if (value != null) UNIT = value.Value; }
        }
        public MSelectItem? PARTItem
        {
            get => Textbook.Parts.SingleOrDefault(o => o.Value == PART);
            set { if (value != null) PART = value.Value; }
        }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MUnitPhraseEdit()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
        }
    }
}
