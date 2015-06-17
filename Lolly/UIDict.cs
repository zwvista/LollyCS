using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lolly
{
    public class UIDict
    {
        public string Name;
        public string Type;
        public DictImage ImageIndex;
    }

    public class UIDictItem : UIDict
    {
    }

    public class UIDictCollection : UIDict
    {
        public bool IsPile => Type == "Pile";
        public List<UIDictItem> Items;
    }
}
