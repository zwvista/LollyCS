using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class MUnitPhrases
    {
        public List<MUnitPhrase> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MUnitPhrase : ReactiveObject, MPhraseInterface
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
        public int PHRASEID { get; set; }
        [JsonProperty]
        [Reactive]
        public string PHRASE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string TRANSLATION { get; set; }
        [Reactive]
        public bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);

        public MUnitPhrase()
        {
        }
    }
    public class MUnitPhraseEdit : ReactiveValidationObject<MUnitPhraseEdit>
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
        public int PHRASEID { get; set; }
        [Reactive]
        public string PHRASE { get; set; } = "";
        [Reactive]
        public string TRANSLATION { get; set; }
        public MTextbook Textbook { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MUnitPhraseEdit()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
