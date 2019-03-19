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
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        string _LANGNAME;
        [JsonProperty("NAME")]
        public string LANGNAME
        {
            get => _LANGNAME;
            set => this.RaiseAndSetIfChanged(ref _LANGNAME, value);
        }
    }
}
