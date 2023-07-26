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
    public class MLangBlogPosts
    {
        [JsonProperty("records")]
        public List<MLangBlogPost> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangBlogPost : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public int GROUPID { get; set; }
        [JsonProperty]
        [Reactive]
        public string GROUPNAME { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string URL { get; set; } = "";
    }
    public class MLangBlogPostEdit : ReactiveValidationObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public int GROUPID { get; set; }
        [Reactive]
        public string GROUPNAME { get; set; }
        [Reactive]
        public string TITLE { get; set; }
        [Reactive]
        public string URL { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangBlogPostEdit()
        {
            this.ValidationRule(x => x.TITLE, v => !string.IsNullOrWhiteSpace(v), "TITLE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
