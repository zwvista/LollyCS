using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MLangPhrases
    {
        public List<MLangPhrase> records { get; set; }
    }
    public class MLangPhrase : ReactiveObject, MPhraseInterface
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        public int PHRASEID => _ID;
        int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get => _LANGID;
            set => this.RaiseAndSetIfChanged(ref _LANGID, value);
        }
        string _PHRASE;
        [JsonProperty]
        public string PHRASE
        {
            get => _PHRASE;
            set => this.RaiseAndSetIfChanged(ref _PHRASE, value);
        }
        string _TRANSLATION;
        [JsonProperty]
        public string TRANSLATION
        {
            get => _TRANSLATION;
            set => this.RaiseAndSetIfChanged(ref _TRANSLATION, value);
        }

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
