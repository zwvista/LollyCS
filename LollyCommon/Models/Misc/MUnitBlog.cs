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
    public class MUnitBlogs
    {
        [JsonProperty("records")]
        public List<MUnitBlog> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MUnitBlog : ReactiveObject
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
        public string CONTENT { get; set; } = "";
    }
}
