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
    public class MLangBlogGPs
    {
        [JsonProperty("records")]
        public List<MLangBlogGP> Records { get; set; } = null!;
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangBlogGP : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int GROUPID { get; set; }
        [JsonProperty]
        [Reactive]
        public int POSTID { get; set; }
        [JsonProperty]
        public string GROUPNAME { get; set; } = "";
        [JsonProperty]
        public string TITLE { get; set; } = "";
        [JsonProperty]
        public string URL { get; set; } = "";
    }
}
