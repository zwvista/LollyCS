using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MSelectItem
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public MSelectItem(int v, string l)
        {
            Value = v;
            Label = l;
        }
    }
    public interface MWordInterface
    {
        int LANGID { get; set; }
        string WORD { get; set; }
        string NOTE { get; set; }
        int FAMIID { get; set; }
        int LEVEL { get; set; }
    }
    public interface MPhraseInterface
    {
        int LANGID { get; set; }
        string PHRASE { get; set; }
        string TRANSLATION { get; set; }
    }
}
