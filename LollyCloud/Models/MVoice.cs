using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MVoices
    {
        public List<MVoice> VVOICES { get; set; }
    }
    public class MVoice
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public int VOICETYPEID { get; set; }
        public string VOICELANG { get; set; }
        public string VOICENAME { get; set; }
    }
}
