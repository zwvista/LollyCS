using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lolly
{
    public class UIDict
    {
    }

    public class UIDictItem : UIDict
    {
        public string Name;
        public string Type;
        public DictImage ImageIndex;
    }

    public class UIDictCollection : UIDict
    {
        public string Name;
        public List<UIDictItem> Items;
    }

    public class UIDictPile : UIDictCollection
    {
    }

    public class UIDictSwitch : UIDictCollection
    {
    }

}
