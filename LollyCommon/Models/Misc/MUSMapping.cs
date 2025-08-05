using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class MUSMappings
    {
        [JsonProperty("records")]
        public List<MUSMapping> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MUSMapping : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string NAME { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int KIND { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int ENTITYID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int VALUEID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LEVEL { get; set; }

        public static string NAME_USLANG = "USLANG";
        public static string NAME_USROWSPERPAGEOPTIONS = "USROWSPERPAGEOPTIONS";
        public static string NAME_USROWSPERPAGE = "USROWSPERPAGE";
        public static string NAME_USLEVELCOLORS = "USLEVELCOLORS";
        public static string NAME_USSCANINTERVAL = "USSCANINTERVAL";
        public static string NAME_USREVIEWINTERVAL = "USREVIEWINTERVAL";
        public static string NAME_USREADNUMBER = "USREADNUMBER";

        public static string NAME_USTEXTBOOK = "USTEXTBOOK";
        public static string NAME_USDICTREFERENCE = "USDICTREFERENCE";
        public static string NAME_USDICTNOTE = "USDICTNOTE";
        public static string NAME_USDICTSREFERENCE = "USDICTSREFERENCE";
        public static string NAME_USDICTTRANSLATION = "USDICTTRANSLATION";
        public static string NAME_USMACVOICE = "USMACVOICE";
        public static string NAME_USIOSVOICE = "USIOSVOICE";
        public static string NAME_USANDROIDVOICE = "USANDROIDVOICE";
        public static string NAME_USWEBVOICE = "USWEBVOICE";
        public static string NAME_USWINDOWSVOICE = "USWINDOWSVOICE";

        public static string NAME_USUNITFROM = "USUNITFROM";
        public static string NAME_USPARTFROM = "USPARTFROM";
        public static string NAME_USUNITTO = "USUNITTO";
        public static string NAME_USPARTTO = "USPARTTO";

    }
}
