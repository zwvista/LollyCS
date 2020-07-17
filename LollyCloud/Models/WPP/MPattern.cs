using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class MPatterns
    {
        [JsonProperty("records")]
        public List<MPattern> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MPattern : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        public int PATTERNID => ID;
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public string PATTERN { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string NOTE { get; set; }
        [JsonProperty]
        [Reactive]
        public string TAGS { get; set; }

        public MPattern()
        {
        }

    }
    public class MPatternEdit : ReactiveValidationObject<MPatternEdit>
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public string PATTERN { get; set; } = "";
        [Reactive]
        public string NOTE { get; set; }
        [Reactive]
        public string TAGS { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MPatternEdit()
        {
            this.ValidationRule(x => x.PATTERN, v => !string.IsNullOrWhiteSpace(v), "PATTERN must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
