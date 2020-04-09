using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MTextbooks
    {
        public List<MTextbook> records { get; set; }
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
}
