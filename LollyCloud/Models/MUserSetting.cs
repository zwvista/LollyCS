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
    [JsonObject(MemberSerialization.OptOut)]
    public class MUserSetting : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int USERID { get; set; }
        [Reactive]
        public int KIND { get; set; }
        [Reactive]
        public int ENTITYID { get; set; }
        [Reactive]
        public string VALUE1 { get; set; }
        [Reactive]
        public string VALUE2 { get; set; }
        [Reactive]
        public string VALUE3 { get; set; }
        [Reactive]
        public string VALUE4 { get; set; }
    }
    public class MUserSettingInfo
    {
        public int USERSETTINGID { get; set; }
        public int VALUEID { get; set; }
    }
}
