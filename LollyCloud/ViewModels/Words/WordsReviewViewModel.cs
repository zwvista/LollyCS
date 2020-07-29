using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class WordsReviewViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        MDictionary DictTranslation => vmSettings.SelectedDictTranslation;

        public List<MUnitWord> Items { get; set; }
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; }
        [Reactive]
        public int Index { get; set; }
        public string IndexCount => $"{Index}/{Count}";
        public bool HasNext => Index < Count;
        public MUnitWord CurrentItem => HasNext ? Items[Index] : null;
        public string CurrentWord => HasNext ? Items[Index].WORD : "";
        public bool IsTestMode => Options.Mode == ReviewMode.Test;
        public MReviewOptions Options { get; set; } = new MReviewOptions();

        public WordsReviewViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }

        public async Task NewTest()
        {
            Items = await unitWordDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
            int nFrom = Count * (Options.GroupSelected - 1) / Options.GroupCount;
            int nTo = Count * Options.GroupSelected / Options.GroupCount;
            Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
            if (Options.Levelge0only)
                Items = Items.Where(o => o.LEVEL >= 0).ToList();
            if (Options.Shuffled)
                Items.Shuffle();
            CorrectIDs = new List<int>();
            Index = 0;
        }
        public void Next()
        {
            Index++;
            if (IsTestMode && !HasNext)
            {
                Index = 0;
                Items = Items.Where(o => !CorrectIDs.Contains(o.ID)).ToList();
            }
        }
        public async Task<string> GetTranslation()
        {
            if (!vmSettings.HasDictTranslation) return "";
            var url = DictTranslation.UrlString(CurrentWord, vmSettings.AutoCorrects);
            var html = await vmSettings.client.GetStringAsync(url);
            return HtmlTransformService.ExtractTextFromHtml(html, DictTranslation.TRANSFORM, "", (text, _) => text);
        }
        public async Task Check(string wordInput)
        {
            if (!HasNext) return;
            var o = CurrentItem;
            var isCorrect = o.WORD == wordInput;
            if (isCorrect) CorrectIDs.Add(o.ID);
            var o2 = await wordFamiDS.Update(o.WORDID, isCorrect);
            o.CORRECT = o2.CORRECT;
            o.TOTAL = o2.TOTAL;
        }
    }
}
