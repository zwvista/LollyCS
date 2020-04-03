using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public class MBlog : ReactiveObject
    {
        [Reactive]
        public string MarkedText { get; set; } = "";
        [Reactive]
        public string HtmlText { get; set; } = "";
        [Reactive]
        public string PatternNo { get; set; } = "001";
        [Reactive]
        public string PatternText { get; set; } = "";
    }
}
