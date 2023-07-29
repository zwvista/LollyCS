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
    public class MLangBlogGroups
    {
        [JsonProperty("records")]
        public List<MLangBlogGroup> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangBlogGroup : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty("NAME")]
        [Reactive]
        public string GROUPNAME { get; set; } = "";
        [JsonIgnore]
        [Reactive]
        public int GPID { get; set; }
    }
    public class MLangBlogGroupEdit : ReactiveValidationObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string GROUPNAME { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangBlogGroupEdit()
        {
            this.ValidationRule(x => x.GROUPNAME, v => !string.IsNullOrWhiteSpace(v), "GROUPNAME must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
