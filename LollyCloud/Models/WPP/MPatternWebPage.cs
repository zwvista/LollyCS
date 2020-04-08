using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class MPatternWebPages
    {
        public List<MPatternWebPage> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MPatternWebPage : ReactiveValidationObject<MPatternWebPage>
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int PATTERNID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PATTERN { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public string WEBPAGE { get; set; } = "";

        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        public MPatternWebPage()
        {
            this.ValidationRule(x => x.WEBPAGE, v => !string.IsNullOrWhiteSpace(v), "WEBPAGE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }

    }
}
