using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCommon
{
    public class MPatterns
    {
        [JsonProperty("records")]
        public List<MPattern> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MPattern : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        public int PATTERNID => ID;
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string PATTERN { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string TAGS { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string URL { get; set; } = "";

        public MPattern()
        {
        }

    }
    public partial class MPatternEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial string PATTERN { get; set; } = "";
        [Reactive]
        public partial string TAGS { get; set; }
        [Reactive]
        public partial string TITLE { get; set; }
        [Reactive]
        public partial string URL { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MPatternEdit()
        {
            this.ValidationRule(x => x.PATTERN, v => !string.IsNullOrWhiteSpace(v), "PATTERN must not be empty");
        }
    }

    public partial class MPatternVariation : ReactiveObject
    {
        [Reactive]
        public partial int Index { get; set; }
        [Reactive]
        public partial string Variation { get; set; }
    }
}
