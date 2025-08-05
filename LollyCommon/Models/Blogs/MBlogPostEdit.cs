using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon
{
    public partial class MBlogPostEdit : ReactiveObject
    {
        [Reactive]
        public partial string MarkedText { get; set; } = "";
        [Reactive]
        public partial string HtmlText { get; set; } = "";
        [Reactive]
        public partial string PatternNo { get; set; } = "001";
    }
}
