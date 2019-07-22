using Newtonsoft.Json;
using ReactiveUI;
using System.Collections.Generic;

namespace LollyShared
{
    public class MWordsFami
    {
        public List<MWordFami> records { get; set; }
    }
    public class MWordFami : ReactiveObject
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
        int _WORDID;
        [JsonProperty]
        public int WORDID
        {
            get => _WORDID;
            set => this.RaiseAndSetIfChanged(ref _WORDID, value);
        }
        int _LEVEL;
        [JsonProperty]
        public int LEVEL
        {
            get => _LEVEL;
            set => this.RaiseAndSetIfChanged(ref _LEVEL, value);
        }
        int _CORRECT;
        [JsonProperty]
        public int CORRECT
        {
            get => _CORRECT;
            set => this.RaiseAndSetIfChanged(ref _CORRECT, value);
        }
        int _TOTAL;
        [JsonProperty]
        public int TOTAL
        {
            get => _TOTAL;
            set => this.RaiseAndSetIfChanged(ref _TOTAL, value);
        }
    }
}
