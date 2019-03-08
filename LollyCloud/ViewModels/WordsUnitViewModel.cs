using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LollyShared
{
    public class WordsUnitViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        public UnitWordDataStore DS = new UnitWordDataStore();

        public ObservableCollection<MUnitWord> UnitWords { get; set; }

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public static async Task<WordsUnitViewModel> CreateAsync(SettingsViewModel vmSettings)
        {
            var o = new WordsUnitViewModel();
            o.vmSettings = vmSettings;
            o.UnitWords = new ObservableCollection<MUnitWord>(await o.DS.GetDataByTextbookUnitPart(
                vmSettings.USTEXTBOOKID, vmSettings.USUNITPARTFROM, vmSettings.USUNITPARTTO,
                vmSettings.Units, vmSettings.Parts));
            return o;
        }
    }
}
