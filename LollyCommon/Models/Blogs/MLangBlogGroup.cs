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
    public class MLangBlogGroups
    {
        [JsonProperty("records")]
        public List<MLangBlogGroup> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MLangBlogGroup : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty("NAME")]
        [Reactive]
        public partial string GROUPNAME { get; set; } = "";
        [JsonIgnore]
        [Reactive]
        public partial int GPID { get; set; }
    }
    public partial class MLangBlogGroupEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial int LANGID { get; set; }
        [Reactive]
        public partial string GROUPNAME { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangBlogGroupEdit()
        {
            this.ValidationRule(x => x.GROUPNAME, v => !string.IsNullOrWhiteSpace(v), "GROUPNAME must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
