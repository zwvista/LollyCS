using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MTransformItem: ReactiveObject
    {
        [Reactive]
        public string Extractor { get; set; }
        [Reactive]
        public string Replacement { get; set; }
    }

}
