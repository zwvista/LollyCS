using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LollyShared
{
    public class WordsReviewViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        MDictTranslation DictTranslation => vmSettings.SelectedDictTranslation;

        public List<MUnitWord> Items { get; set; }
        public int Count => Items.Count;
        public List<int> CorrectIDs { get; set; }
        int _Index;
        public int Index
        {
            get => _Index;
            set => this.RaiseAndSetIfChanged(ref _Index, value);
        }
        public string IndexCount => $"{Index}/{Count}";
        public bool HasNext => Index < Count;
        public MUnitWord CurrentItem => HasNext ? Items[Index] : null;
        public string CurrentWord => HasNext ? Items[Index].WORD : "";
        public ReviewMode Mode { get; set; } = ReviewMode.ReviewAuto;
        public bool IsTestMode => Mode == ReviewMode.Test;
        public MReviewOptions Options { get; set; } = new MReviewOptions();

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public static async Task<WordsReviewViewModel> CreateAsync(SettingsViewModel vmSettings, bool needCopy)
        {
            var o = new WordsReviewViewModel();
            o.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            return o;
        }

        public async Task NewTest()
        {
            Items = await unitWordDS.GetDataByTextbookUnitPart(
                vmSettings.SelectedTextbook, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO);
            if (Options.Levelge0only)
                Items = Items.Where(o => o.LEVEL >= 0).ToList();
            int nFrom = Count * (Options.GroupSelected - 1) / Options.GroupCount;
            int nTo = Count * Options.GroupSelected / Options.GroupCount;
            Items = Items.Skip(nFrom).Take(nTo - nFrom).ToList();
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
                Items = Items.Where(o => CorrectIDs.Contains(o.ID)).ToList();
            }
        }
        public async Task<string> GetTranslation()
        {
            if (!vmSettings.HasDictTranslation) return "";
            var url = DictTranslation.UrlString(CurrentWord, vmSettings.AutoCorrects);
            var html = await vmSettings.client.GetStringAsync(url);
            return CommonApi.ExtractTextFromHtml(html, DictTranslation.TRANSFORM, "", (text, _) => text);
        }
        public async void Check(string wordInput)
        {
            if (!HasNext) return;
            var o = CurrentItem;
            var isCorrect = o.WORD == wordInput;
            if (isCorrect) CorrectIDs.Add(o.ID);
            await wordFamiDS.Update(o.WORDID, o.LEVEL);
        }
    }
}
