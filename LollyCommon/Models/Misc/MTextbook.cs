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
    public class MTextbooks
    {
        [JsonProperty("records")]
        public List<MTextbook> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MTextbook : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty("NAME")]
        [Reactive]
        public partial string TEXTBOOKNAME { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string UNITS { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string PARTS { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial bool ONLINE { get; set; }

        [Reactive]
        public partial List<MSelectItem> Units { get; set; }
        [Reactive]
        public partial List<MSelectItem> Parts { get; set; }

        public string UNITSTR(int UNIT) => Units.First(o => o.Value == UNIT).Label;
        public string PARTSTR(int PART) => Parts.First(o => o.Value == PART).Label;
    }
    public partial class MTextbookEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial string TEXTBOOKNAME { get; set; }
        [Reactive]
        public partial string UNITS { get; set; }
        [Reactive]
        public partial string PARTS { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MTextbookEdit()
        {
            this.ValidationRule(x => x.TEXTBOOKNAME, v => !string.IsNullOrWhiteSpace(v), "TEXTBOOKNAME must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
