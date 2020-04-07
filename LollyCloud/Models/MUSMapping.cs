using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MUSMappings
    {
        public List<MUSMapping> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MUSMapping : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public string NAME { get; set; }
        [Reactive]
        public int KIND { get; set; }
        [Reactive]
        public int ENTITYID { get; set; }
        [Reactive]
        public int VALUEID { get; set; }
        [Reactive]
        public int LEVEL { get; set; }

        public static string NAME_USLANGID = "USLANGID";
        public static string NAME_USROWSPERPAGEOPTIONS = "USROWSPERPAGEOPTIONS";
        public static string NAME_USROWSPERPAGE = "USROWSPERPAGE";
        public static string NAME_USLEVELCOLORS = "USLEVELCOLORS";
        public static string NAME_USSCANINTERVAL = "USSCANINTERVAL";
        public static string NAME_USREVIEWINTERVAL = "USREVIEWINTERVAL";
        public static string NAME_USREADNUMBERID = "USREADNUMBERID";

        public static string NAME_USTEXTBOOKID = "USTEXTBOOKID";
        public static string NAME_USDICTREFERENCE = "USDICTREFERENCE";
        public static string NAME_USDICTNOTE = "USDICTNOTE";
        public static string NAME_USDICTSREFERENCE = "USDICTSREFERENCE";
        public static string NAME_USDICTTRANSLATION = "USDICTTRANSLATION";
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
