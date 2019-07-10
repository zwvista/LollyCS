using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MUSMappings
    {
        public List<MUSMapping> records { get; set; }
    }
    public class MUSMapping : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        string _NAME;
        [JsonProperty]
        public string NAME
        {
            get => _NAME;
            set => this.RaiseAndSetIfChanged(ref _NAME, value);
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
        int _VALUEID;
        [JsonProperty]
        public int VALUEID
        {
            get => _VALUEID;
            set => this.RaiseAndSetIfChanged(ref _VALUEID, value);
        }
        int _LEVEL;
        [JsonProperty]
        public int LEVEL
        {
            get => _LEVEL;
            set => this.RaiseAndSetIfChanged(ref _LEVEL, value);
        }

        public static string NAME_USLANGID = "USLANGID";
        public static string NAME_USROWSPERPAGEOPTIONS = "USROWSPERPAGEOPTIONS";
        public static string NAME_USROWSPERPAGE = "USROWSPERPAGE";
        public static string NAME_USLEVELCOLORS = "USLEVELCOLORS";
        public static string NAME_USSCANINTERVAL = "USSCANINTERVAL";
        public static string NAME_USREVIEWINTERVAL = "USREVIEWINTERVAL";

        public static string NAME_USTEXTBOOKID = "USTEXTBOOKID";
        public static string NAME_USDICTITEM = "USDICTITEM";
        public static string NAME_USDICTNOTEID = "USDICTNOTEID";
        public static string NAME_USDICTITEMS = "USDICTITEMS";
        public static string NAME_USDICTTRANSLATIONID = "USDICTTRANSLATIONID";
        public static string NAME_USMACVOICEID = "USMACVOICEID";
        public static string NAME_USIOSVOICEID = "USIOSVOICEID";
        public static string NAME_USANDROIDVOICEID = "USANDROIDVOICEID";
        public static string NAME_USWEBVOICEID = "USWEBVOICEID";
        public static string NAME_USWINDOWSVOICEID = "USWINDOWSVOICEID";

        public static string NAME_USUNITFROM = "USUNITFROM";
        public static string NAME_USPARTFROM = "USPARTFROM";
        public static string NAME_USUNITTO = "USUNITTO";
        public static string NAME_USPARTTO = "USPARTTO";

    }
}
