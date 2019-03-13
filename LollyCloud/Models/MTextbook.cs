using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MTextbooks
    {
        public List<MTextbook> TEXTBOOKS { get; set; }
    }
    public class MTextbook : ReactiveObject
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
        private string _TEXTBOOKNAME;
        [JsonProperty("NAME")]
        public string TEXTBOOKNAME
        {
            get { return _TEXTBOOKNAME; }
            set { this.RaiseAndSetIfChanged(ref _TEXTBOOKNAME, value); }
        }
        private string _UNITS;
        [JsonProperty]
        public string UNITS
        {
            get { return _UNITS; }
            set { this.RaiseAndSetIfChanged(ref _UNITS, value); }
        }
        private string _PARTS;
        [JsonProperty]
        public string PARTS
        {
            get { return _PARTS; }
            set { this.RaiseAndSetIfChanged(ref _PARTS, value); }
        }

        public ObservableCollection<MSelectItem> lstUnits;
        public ObservableCollection<MSelectItem> lstParts;
    }
}
