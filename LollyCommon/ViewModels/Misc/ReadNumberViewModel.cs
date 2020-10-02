using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class ReadNumberViewModel : MReadNumber
    {
        public SettingsViewModel vmSettings;
        public ReactiveCommand<Unit, Unit> ReadNumberCommand { get; }
        public ReadNumberViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            Type = (ReadNumberType)this.vmSettings.USREADNUMBER;
            ReadNumberCommand = ReactiveCommand.Create(() =>
            {
                Text =
                    Type == ReadNumberType.Japanese ? ReadNumberService.ReadInJapanese(Number) :
                    Type == ReadNumberType.KoreanNative ? ReadNumberService.ReadInNativeKorean(Number) :
                    Type == ReadNumberType.KoreanSino ? ReadNumberService.ReadInSinoKorean(Number) :
                    "";
            });
        }
    }
}
