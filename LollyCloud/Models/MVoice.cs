using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MVoices
    {
        public List<MVoice> VVOICES { get; set; }
    }
    public class MVoice : ReactiveObject
    {
        private int _ID;
        [JsonProperty]
        public int ID
        {
            get { return _ID; }
            set { this.RaiseAndSetIfChanged(ref _ID, value); }
        }
        private int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get { return _LANGID; }
            set { this.RaiseAndSetIfChanged(ref _LANGID, value); }
        }
        private int _VOICETYPEID;
        [JsonProperty]
        public int VOICETYPEID
        {
            get { return _VOICETYPEID; }
            set { this.RaiseAndSetIfChanged(ref _VOICETYPEID, value); }
        }
        private string _VOICELANG;
        [JsonProperty]
        public string VOICELANG
        {
            get { return _VOICELANG; }
            set { this.RaiseAndSetIfChanged(ref _VOICELANG, value); }
        }
        private string _VOICENAME;
        [JsonProperty]
        public string VOICENAME
        {
            get { return _VOICENAME; }
            set { this.RaiseAndSetIfChanged(ref _VOICENAME, value); }
        }
    }
}
