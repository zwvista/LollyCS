using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LollyShared
{
    public class ReadNumberViewModel : MReadNumber
    {
        public SettingsViewModel vmSettings;
        public ReadNumberViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }
    }
}
