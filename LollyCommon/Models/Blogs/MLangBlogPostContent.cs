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
    public class MLangBlogPostContents
    {
        [JsonProperty("records")]
        public List<MLangBlogPostContent> Records { get; set; } = null!;
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MLangBlogPostContent : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string CONTENT { get; set; } = "";
    }
}
