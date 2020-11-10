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
    public class MWebTextbooks
    {
        [JsonProperty("records")]
        public List<MWebTextbook> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MWebTextbook : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int TEXTBOOKID { get; set; }
        [JsonProperty]
        [Reactive]
        public int UNIT { get; set; }
        [JsonProperty]
        [Reactive]
        public int WEBPAGEID { get; set; }
    }
}
