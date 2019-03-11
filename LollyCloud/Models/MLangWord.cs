using System;
using System.Collections.Generic;
using System.Linq;

namespace LollyShared
{
    public class MLangWords
    {
        public List<MLangWord> VLANGWORDS { get; set; }
    }
    public class MLangWord
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public string WORD { get; set; }
        public string NOTE { get; set; }
        public int FAMIID { get; set; }
        public int LEVEL { get; set; }

        MLangWord() { }
        MLangWord(MUnitWord item)
        {
            ID = item.WORDID;
            LANGID = item.LANGID;
            WORD = item.WORD;
            NOTE = item.NOTE;
        }

        bool CombineNote(string note)
        {
            var oldNote = NOTE;
            if (!string.IsNullOrEmpty(note))
                if (string.IsNullOrEmpty(NOTE))
                    NOTE = note;
                else
                {
                    var lst = NOTE.Split(',').ToList();
                    if (!lst.Contains(note))
                        lst.Add(note);
                    NOTE = string.Join(",", lst);
                }
            return oldNote != NOTE;
        }
    }
}
