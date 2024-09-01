using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class MOnlineTextbooks
    {
        [JsonProperty("records")]
        public List<MOnlineTextbook> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MOnlineTextbook : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int TEXTBOOKID { get; set; }
        [JsonProperty]
        [Reactive]
        public string TEXTBOOKNAME { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public int UNIT { get; set; }
        [JsonProperty]
        [Reactive]
        public string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string URL { get; set; } = "";
    }
}
