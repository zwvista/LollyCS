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
    public class MLangBlogPostContents
    {
        [JsonProperty("records")]
        public List<MLangBlogPostContent> Records { get; set; } = null!;
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangBlogPostContent : ReactiveObject
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
