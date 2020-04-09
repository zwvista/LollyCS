using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MUserSettings
    {
        public List<MUserSetting> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MUserSetting : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int USERID { get; set; }
        [JsonProperty]
        [Reactive]
        public int KIND { get; set; }
        [JsonProperty]
        [Reactive]
        public int ENTITYID { get; set; }
        [JsonProperty]
        [Reactive]
        public string VALUE1 { get; set; }
        [JsonProperty]
        [Reactive]
        public string VALUE2 { get; set; }
        [JsonProperty]
        [Reactive]
        public string VALUE3 { get; set; }
        [JsonProperty]
        [Reactive]
        public string VALUE4 { get; set; }
    }
    public class MUserSettingInfo
    {
        public int USERSETTINGID { get; set; }
        public int VALUEID { get; set; }
    }
}
