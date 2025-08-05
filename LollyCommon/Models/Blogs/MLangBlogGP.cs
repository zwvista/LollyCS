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
    public class MLangBlogGPs
    {
        [JsonProperty("records")]
        public List<MLangBlogGP> Records { get; set; } = null!;
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MLangBlogGP : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int GROUPID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int POSTID { get; set; }
        [JsonProperty]
        public string GROUPNAME { get; set; } = "";
        [JsonProperty]
        public string TITLE { get; set; } = "";
        [JsonProperty]
        public string URL { get; set; } = "";
    }
}
