using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public enum ReadNumberType
    {
        Japanese = 1,
        KoreanNative,
        KoreanSino,
    }
    public class MReadNumber : ReactiveObject
    {
        [Reactive]
        public int Number { get; set; }
        [Reactive]
        public string Text { get; set; }
        [Reactive]
        public ReadNumberType Type { get; set; }
    }
}
