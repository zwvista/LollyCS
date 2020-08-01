using System.Collections.ObjectModel;
using System.Linq;

namespace LollyCloud
{
    public class SettingsDictsViewModel
    {
        SettingsViewModel vmSettings;
        public ObservableCollection<MDictionary> DictsAvailable { get; }
        public ObservableCollection<MDictionary> DictsSelected { get; }
        public SettingsDictsViewModel(SettingsViewModel vmSettings)
        {
            this.vmSettings = vmSettings;
            DictsSelected = new ObservableCollection<MDictionary>(vmSettings.SelectedDictsReference);
            DictsAvailable = new ObservableCollection<MDictionary>(vmSettings.DictsReference.Except(vmSettings.SelectedDictsReference));
        }
        public async void OnOK() =>
            await vmSettings.UpdateDictsReference(DictsSelected.ToList());
    }
}
