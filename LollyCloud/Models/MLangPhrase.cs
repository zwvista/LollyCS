using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MLangPhrases
    {
        public List<MLangPhrase> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MLangPhrase : ReactiveObject, MPhraseInterface
    {
        [Reactive]
        public int ID { get; set; }
        public int PHRASEID => ID;
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PHRASE { get; set; }
        [Reactive]
        public string TRANSLATION { get; set; }

        public MLangPhrase() { }
        public MLangPhrase(MUnitPhrase item)
        {
            ID = item.PHRASEID;
            LANGID = item.LANGID;
            PHRASE = item.PHRASE;
            TRANSLATION = item.TRANSLATION;
        }

        public bool CombineTranslation(string translation)
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
