using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public class SelectDictsViewModel
    {
        SettingsViewModel vmSettings;
        public ObservableCollection<MDictionary> DictsAvailable { get; }
        public ObservableCollection<MDictionary> DictsSelected { get; }
        public SelectDictsViewModel(SettingsViewModel vmSettings)
        {
            this.vmSettings = vmSettings;
            DictsSelected = new ObservableCollection<MDictionary>();
            DictsAvailable = new ObservableCollection<MDictionary>(vmSettings.DictsReference);
        }
    }
}
