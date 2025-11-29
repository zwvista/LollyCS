using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class MUsers
    {
        [JsonProperty("records")]
        public List<MUser> Records { get; set; } = null!;
    }

    [JsonObject(MemberSerialization.OptIn)]
    public partial class MUser : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string USERID { get; set; } = "";
        [JsonProperty]
        [Reactive]
        [Required(ErrorMessage = "Username is required")]
        public partial string USERNAME { get; set; } = "";
        [JsonProperty]
        [Reactive]
        [Required(ErrorMessage = "Password is required")]
        public partial string PASSWORD { get; set; } = "";
    }

}
