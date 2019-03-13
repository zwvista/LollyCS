using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MLanguages
    {
        public List<MLanguage> LANGUAGES { get; set; }
    }
    public class MLanguage : ReactiveObject
    {
        private int _ID;
        [JsonProperty]
        public int ID
        {
            get { return _ID; }
            set { this.RaiseAndSetIfChanged(ref _ID, value); }
        }
        private string _LANGNAME;
        [JsonProperty("NAME")]
        public string LANGNAME
        {
            get { return _LANGNAME; }
            set { this.RaiseAndSetIfChanged(ref _LANGNAME, value); }
        }
    }
}
