using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MUserSettings
    {
        public List<MUserSetting> records { get; set; }
    }
    public class MUserSetting : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int USERID { get; set; }
        [Reactive]
        [JsonProperty]
        public int KIND { get; set; }
        [Reactive]
        [JsonProperty]
        public int ENTITYID { get; set; }
        [Reactive]
        [JsonProperty]
        public string VALUE1 { get; set; }
        [Reactive]
        [JsonProperty]
        public string VALUE2 { get; set; }
        [Reactive]
        [JsonProperty]
        public string VALUE3 { get; set; }
        [Reactive]
        [JsonProperty]
        public string VALUE4 { get; set; }
    }
    public class MUserSettingInfo
    {
        public int USERSETTINGID { get; set; }
        public int VALUEID { get; set; }
    }
}
