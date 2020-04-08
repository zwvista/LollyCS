using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class MPatternPhrases
    {
        public List<MPatternPhrase> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MPatternPhrase : ReactiveValidationObject<MPatternPhrase>
    {
        [Reactive]
        public int PATTERNID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PATTERN { get; set; }
        [Reactive]
        public string NOTE { get; set; }
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public int PHRASEID { get; set; }
        [Reactive]
        public string PHRASE { get; set; } = "";
        [Reactive]
        public string TRANSLATION { get; set; }

        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        public MPatternPhrase()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }

    }
}
