using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCloud
{
    public class MLangPhrases
    {
        public List<MLangPhrase> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MLangPhrase : ReactiveValidationObject<MLangPhrase>, MPhraseInterface
    {
        [Reactive]
        public int ID { get; set; }
        public int PHRASEID => ID;
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string PHRASE { get; set; } = "";
        [Reactive]
        public string TRANSLATION { get; set; }

        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        void WhenAnyValueChanged()
        {
            this.ValidationRule(x => x.PHRASE, v => !string.IsNullOrWhiteSpace(v), "PHRASE must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }

        public MLangPhrase()
        {
            WhenAnyValueChanged();
        }
        public MLangPhrase(MUnitPhrase item)
        {
            ID = item.PHRASEID;
            LANGID = item.LANGID;
            PHRASE = item.PHRASE;
            TRANSLATION = item.TRANSLATION;
            WhenAnyValueChanged();
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
