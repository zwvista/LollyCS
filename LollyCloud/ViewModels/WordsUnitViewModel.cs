using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class WordsUnitViewModel : LollyViewModel
    {
        public SettingsViewModel SettingsVM;
        public UnitWordDataStore DS = new UnitWordDataStore();
 
        public ObservableCollection<UnitWord> UnitWords { get; set; }
        public Command GetDataCommand { get; set; }
        public Command CreateCommand { get; set; }

        public WordsUnitViewModel(SettingsViewModel SettingsVM)
        {
            this.SettingsVM = SettingsVM;
            GetDataCommand = new Command(async () => UnitWords = await GetData(async () => 
                await DS.GetDataByTextbookUnitPart(SettingsVM.USTEXTBOOKID, SettingsVM.USUNITPARTFROM, SettingsVM.USUNITPARTTO)));
            CreateCommand = new Command<UnitWord>(async (UnitWord item) => {
                UnitWords.Add(item);
                await DS.Create(item);
            });
        }
    }
}
