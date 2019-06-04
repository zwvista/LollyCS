using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MUserSettings
    {
        public List<MUserSetting> records { get; set; }
    }
    public class MUserSetting : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        int _USERID;
        [JsonProperty]
        public int USERID
        {
            get => _USERID;
            set => this.RaiseAndSetIfChanged(ref _USERID, value);
        }
        int _KIND;
        [JsonProperty]
        public int KIND
        {
            get => _KIND;
            set => this.RaiseAndSetIfChanged(ref _KIND, value);
        }
        int _ENTITYID;
        [JsonProperty]
        public int ENTITYID
        {
            get => _ENTITYID;
            set => this.RaiseAndSetIfChanged(ref _ENTITYID, value);
        }
        string _VALUE1;
        [JsonProperty]
        public string VALUE1
        {
            get => _VALUE1;
            set => this.RaiseAndSetIfChanged(ref _VALUE1, value);
        }
        string _VALUE2;
        [JsonProperty]
        public string VALUE2
        {
            get => _VALUE2;
            set => this.RaiseAndSetIfChanged(ref _VALUE2, value);
        }
        string _VALUE3;
        [JsonProperty]
        public string VALUE3
        {
            get => _VALUE3;
            set => this.RaiseAndSetIfChanged(ref _VALUE3, value);
        }
        string _VALUE4;
        [JsonProperty]
        public string VALUE4
        {
            get => _VALUE4;
            set => this.RaiseAndSetIfChanged(ref _VALUE4, value);
        }
    }
}
