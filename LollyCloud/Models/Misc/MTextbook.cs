using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCloud
{
    public class MTextbooks
    {
        [JsonProperty("records")]
        public List<MTextbook> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MTextbook : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty("NAME")]
        [Reactive]
        public string TEXTBOOKNAME { get; set; }
        [JsonProperty]
        [Reactive]
        public string UNITS { get; set; }
        [JsonProperty]
        [Reactive]
        public string PARTS { get; set; }

        [Reactive]
        public List<MSelectItem> Units { get; set; }
        [Reactive]
        public List<MSelectItem> Parts { get; set; }

        public string UNITSTR(int UNIT) => Units.First(o => o.Value == UNIT).Label;
        public string PARTSTR(int PART) => Parts.First(o => o.Value == PART).Label;
    }
    public class MTextbookEdit : ReactiveValidationObject<MTextbookEdit>
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public string TEXTBOOKNAME { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MTextbookEdit()
        {
            this.ValidationRule(x => x.TEXTBOOKNAME, v => !string.IsNullOrWhiteSpace(v), "TEXTBOOKNAME must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
