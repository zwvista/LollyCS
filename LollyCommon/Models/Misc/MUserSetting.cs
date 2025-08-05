using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class MUserSettings
    {
        [JsonProperty("records")]
        public List<MUserSetting> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MUserSetting : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string USERID { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int KIND { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int ENTITYID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string VALUE1 { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string VALUE2 { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string VALUE3 { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string VALUE4 { get; set; }
    }
    public class MUserSettingInfo
    {
        public int USERSETTINGID { get; set; }
        public int VALUEID { get; set; }
    }
}
