using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MUserSettings
    {
        public List<MUserSetting> USERSETTINGS { get; set; }
    }
    public class MUserSetting : ReactiveObject
    {
        private int _ID;
        [JsonProperty]
        public int ID
        {
            get { return _ID; }
            set { this.RaiseAndSetIfChanged(ref _ID, value); }
        }
        private int _USERID;
        [JsonProperty]
        public int USERID
        {
            get { return _USERID; }
            set { this.RaiseAndSetIfChanged(ref _USERID, value); }
        }
        private int _KIND;
        [JsonProperty]
        public int KIND
        {
            get { return _KIND; }
            set { this.RaiseAndSetIfChanged(ref _KIND, value); }
        }
        private int _ENTITYID;
        [JsonProperty]
        public int ENTITYID
        {
            get { return _ENTITYID; }
            set { this.RaiseAndSetIfChanged(ref _ENTITYID, value); }
        }
        private string _VALUE1;
        [JsonProperty]
        public string VALUE1
        {
            get { return _VALUE1; }
            set { this.RaiseAndSetIfChanged(ref _VALUE1, value); }
        }
        private string _VALUE2;
        [JsonProperty]
        public string VALUE2
        {
            get { return _VALUE2; }
            set { this.RaiseAndSetIfChanged(ref _VALUE2, value); }
        }
        private string _VALUE3;
        [JsonProperty]
        public string VALUE3
        {
            get { return _VALUE3; }
            set { this.RaiseAndSetIfChanged(ref _VALUE3, value); }
        }
        private string _VALUE4;
        [JsonProperty]
        public string VALUE4
        {
            get { return _VALUE4; }
            set { this.RaiseAndSetIfChanged(ref _VALUE4, value); }
        }
    }
}
