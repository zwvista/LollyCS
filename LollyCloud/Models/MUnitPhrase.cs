using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class MUnitPhrases
    {
        public List<MUnitPhrase> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MUnitPhrase : ReactiveObject, MPhraseInterface, IValidatableViewModel
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public int TEXTBOOKID { get; set; }
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
        [Reactive]
        public bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);

        public ValidationContext ValidationContext { get; } = new ValidationContext();
        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        public MUnitPhrase()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
