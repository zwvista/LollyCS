using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon.ViewModels
{
    public class SearchViewModel : ReactiveObject
    {
        public OnlineDictViewModel vmDict { get; set; }
        public SettingsViewModel vmSettings { get; set; }
        public SearchViewModel(SettingsViewModel vmSettings, IOnlineDict dict)
        {
            this.vmSettings = vmSettings;
            vmDict = new OnlineDictViewModel(vmSettings, dict);
            vmSettings.WhenAnyValue(x => x.SelectedDictReference).Where(v => v != null).Subscribe(async v =>
            {
                vmDict.Dict = v;
                await vmDict.SearchDict();
            });
            vmDict.WhenAnyValue(x => x.Word).Skip(1).Subscribe(async v =>
            {
                await vmDict.SearchDict();
            });
        }
    }
}
