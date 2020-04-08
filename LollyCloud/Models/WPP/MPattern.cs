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
    [JsonObject(MemberSerialization.OptOut)]
    public class MPattern : ReactiveValidationObject<MPattern>
    {
        [Reactive]
        public int ID { get; set; }
        public int PATTERNID => ID;
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PATTERN { get; set; } = "";
        [Reactive]
        public string NOTE { get; set; }

        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        public MPattern()
        {
            this.ValidationRule(x => x.PATTERN, v => !string.IsNullOrWhiteSpace(v), "PATTERN must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }

    }
}
