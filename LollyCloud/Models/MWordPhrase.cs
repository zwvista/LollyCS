using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MWordsPhrases
    {
        public List<MWordPhrase> records { get; set; }
    }
    public class MWordPhrase : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        int _WORDID;
        public int WORDID
        {
            get => _WORDID;
            set => this.RaiseAndSetIfChanged(ref _WORDID, value);
        }
        int _PHRASEID;
        public int PHRASEID
        {
            get => _PHRASEID;
            set => this.RaiseAndSetIfChanged(ref _PHRASEID, value);
        }

    }
}
