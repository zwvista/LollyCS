using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class MLangPhrases
    {
        [JsonProperty("records")]
        public List<MLangPhrase> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MLangPhrase : ReactiveObject, MPhraseInterface
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        public int PHRASEID => ID;
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string PHRASE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string TRANSLATION { get; set; } = "";
        [Reactive]
        public partial bool IsChecked { get; set; }

        public MLangPhrase()
        {
        }
        public MLangPhrase(MUnitPhrase item)
        {
            ID = item.PHRASEID;
            LANGID = item.LANGID;
            PHRASE = item.PHRASE;
            TRANSLATION = item.TRANSLATION;
        }

        public bool MergeTranslation(string translation)
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
    public partial class MLangPhraseEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial string PHRASE { get; set; } = "";
        [Reactive]
        public partial string TRANSLATION { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangPhraseEdit()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
        }
    }
}
