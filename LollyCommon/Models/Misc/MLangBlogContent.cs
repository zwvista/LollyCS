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
    public class MLangBlogsContent
    {
        [JsonProperty("records")]
        public List<MLangBlogContent> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangBlogContent : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string CONTENT { get; set; } = "";
    }
}
