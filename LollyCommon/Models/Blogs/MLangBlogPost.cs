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
    public class MLangBlogPosts
    {
        [JsonProperty("records")]
        public List<MLangBlogPost> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MLangBlogPost : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string TITLE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string URL { get; set; } = "";
        [JsonIgnore]
        [Reactive]
        public partial int GPID { get; set; }
    }
    public partial class MLangBlogPostEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial int LANGID { get; set; }
        [Reactive]
        public partial string TITLE { get; set; }
        [Reactive]
        public partial string URL { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangBlogPostEdit()
        {
            this.ValidationRule(x => x.TITLE, v => !string.IsNullOrWhiteSpace(v), "TITLE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
