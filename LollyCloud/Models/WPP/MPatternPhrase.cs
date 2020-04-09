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
    [JsonObject(MemberSerialization.OptIn)]
    public class MPatternPhrase : ReactiveValidationObject<MPatternPhrase>
    {
        [JsonProperty]
        [Reactive]
        public int PATTERNID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public string PATTERN { get; set; }
        [JsonProperty]
        [Reactive]
        public string NOTE { get; set; }
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
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

        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        public MPatternPhrase()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }

    }
}
