using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MTextbooks
    {
        public List<MTextbook> TEXTBOOKS { get; set; }
    }
    public class MTextbook : ReactiveObject
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
        string _TEXTBOOKNAME;
        [JsonProperty("NAME")]
        public string TEXTBOOKNAME
        {
            get => _TEXTBOOKNAME;
            set => this.RaiseAndSetIfChanged(ref _TEXTBOOKNAME, value);
        }
        string _UNITS;
        [JsonProperty]
        public string UNITS
        {
            get => _UNITS;
            set => this.RaiseAndSetIfChanged(ref _UNITS, value);
        }
        string _PARTS;
        [JsonProperty]
        public string PARTS
        {
            get => _PARTS;
            set => this.RaiseAndSetIfChanged(ref _PARTS, value);
        }

        List<MSelectItem> _Units;
        public List<MSelectItem> Units
        {
            get => _Units;
            set => this.RaiseAndSetIfChanged(ref _Units, value);
        }
        List<MSelectItem> _Parts;
        public List<MSelectItem> Parts
        {
            get => _Parts;
            set => this.RaiseAndSetIfChanged(ref _Parts, value);
        }

        public string UNITSTR(int UNIT) => Units.First(o => o.Value == UNIT).Label;
        public string PARTSTR(int PART) => Parts.First(o => o.Value == PART).Label;
    }
}
