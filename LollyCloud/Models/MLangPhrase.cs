using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MLangPhrases
    {
        public List<MLangPhrase> LANGPHRASES { get; set; }
    }
    public class MLangPhrase : ReactiveObject
    {
        private int _ID;
        [JsonProperty]
        public int ID
        {
            get { return _ID; }
            set { this.RaiseAndSetIfChanged(ref _ID, value); }
        }
        private int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get { return _LANGID; }
            set { this.RaiseAndSetIfChanged(ref _LANGID, value); }
        }
        private string _PHRASE;
        [JsonProperty]
        public string PHRASE
        {
            get { return _PHRASE; }
            set { this.RaiseAndSetIfChanged(ref _PHRASE, value); }
        }
        private string _TRANSLATION;
        [JsonProperty]
        public string TRANSLATION
        {
            get { return _TRANSLATION; }
            set { this.RaiseAndSetIfChanged(ref _TRANSLATION, value); }
        }

        public MLangPhrase() { }
        public MLangPhrase(MUnitPhrase item)
        {
            ID = item.PHRASEID;
            LANGID = item.LANGID;
            PHRASE = item.PHRASE;
            TRANSLATION = item.TRANSLATION;
        }

        public bool combineTranslation(string translation)
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
