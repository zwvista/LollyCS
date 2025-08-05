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
    public class MUnitBlogPosts
    {
        [JsonProperty("records")]
        public List<MUnitBlogPost> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MUnitBlogPost : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int TEXTBOOKID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int UNIT { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string CONTENT { get; set; } = "";
    }
}
