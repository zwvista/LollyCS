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
    public class MLangBlogs
    {
        [JsonProperty("records")]
        public List<MLangBlog> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangBlog : ReactiveObject
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
    }
    public class MLangBlogEdit : ReactiveValidationObject
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
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangBlogEdit()
        {
            this.ValidationRule(x => x.TITLE, v => !string.IsNullOrWhiteSpace(v), "TITLE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
