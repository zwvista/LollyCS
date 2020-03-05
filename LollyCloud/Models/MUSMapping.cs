using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MUSMappings
    {
        public List<MUSMapping> records { get; set; }
    }
    public class MUSMapping : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public string NAME { get; set; }
        [Reactive]
        [JsonProperty]
        public int KIND { get; set; }
        [Reactive]
        [JsonProperty]
        public int ENTITYID { get; set; }
        [Reactive]
        [JsonProperty]
        public int VALUEID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LEVEL { get; set; }

        public static string NAME_USLANGID = "USLANGID";
        public static string NAME_USROWSPERPAGEOPTIONS = "USROWSPERPAGEOPTIONS";
        public static string NAME_USROWSPERPAGE = "USROWSPERPAGE";
        public static string NAME_USLEVELCOLORS = "USLEVELCOLORS";
        public static string NAME_USSCANINTERVAL = "USSCANINTERVAL";
        public static string NAME_USREVIEWINTERVAL = "USREVIEWINTERVAL";
        public static string NAME_USREADNUMBERID = "USREADNUMBERID";

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
