using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MWordsFami
    {
        public List<MWordFami> WORDSFAMI { get; set; }
    }
    public class MWordFami
    {
        public int ID { get; set; }
        public int USERID { get; set; }
        public int WORDID { get; set; }
        public int LEVEL { get; set; }
    }
}
