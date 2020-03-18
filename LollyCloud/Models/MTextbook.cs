using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MTextbooks
    {
        public List<MTextbook> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MTextbook : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        [JsonProperty("NAME")]
        public string TEXTBOOKNAME { get; set; }
        [Reactive]
        public string UNITS { get; set; }
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
