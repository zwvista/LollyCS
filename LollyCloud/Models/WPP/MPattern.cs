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
        public List<MPattern> records { get; set; }
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

        public MPattern()
        {
        }

    }
    public class MPattern2 : ReactiveValidationObject2<MPattern>
    {
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MPattern2()
        {
            this.ValidationRule(x => x.VM.PATTERN, v => !string.IsNullOrWhiteSpace(v), "PATTERN must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
