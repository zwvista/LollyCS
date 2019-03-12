using System;
using System.Collections.Generic;
using System.Linq;

namespace LollyShared
{
    public class MLangPhrases
    {
        public List<MLangPhrase> LANGPHRASES { get; set; }
    }
    public class MLangPhrase
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public string PHRASE { get; set; }
        public string TRANSLATION { get; set; }

        MLangPhrase(MUnitPhrase item)
        {
            ID = item.PHRASEID;
            LANGID = item.LANGID;
            PHRASE = item.PHRASE;
            TRANSLATION = item.TRANSLATION;
        }

        bool combineTranslation(string translation)
        {
            var oldTranslation = TRANSLATION;
            if (!string.IsNullOrEmpty(translation))
                if (string.IsNullOrEmpty(TRANSLATION))
                    TRANSLATION = translation;
                else
                {
                    var lst = TRANSLATION.Split(',').ToList();
                    if (!lst.Contains(translation))
                        lst.Add(translation);
                    TRANSLATION = string.Join(",", lst);
                }
            return oldTranslation != TRANSLATION;
        }
    }
}
