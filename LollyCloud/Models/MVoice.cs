using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MVoices
    {
        public List<MVoice> records { get; set; }
    }
    public class MVoice : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get => _LANGID;
            set => this.RaiseAndSetIfChanged(ref _LANGID, value);
        }
        int _VOICETYPEID;
        [JsonProperty]
        public int VOICETYPEID
        {
            get => _VOICETYPEID;
            set => this.RaiseAndSetIfChanged(ref _VOICETYPEID, value);
        }
        string _VOICELANG;
        [JsonProperty]
        public string VOICELANG
        {
            get => _VOICELANG;
            set => this.RaiseAndSetIfChanged(ref _VOICELANG, value);
        }
        string _VOICENAME;
        [JsonProperty]
        public string VOICENAME
        {
            get => _VOICENAME;
            set => this.RaiseAndSetIfChanged(ref _VOICENAME, value);
        }
    }
}
