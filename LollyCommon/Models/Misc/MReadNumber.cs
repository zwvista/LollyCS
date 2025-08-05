using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public enum ReadNumberType
    {
        Japanese = 1,
        KoreanNative,
        KoreanSino,
    }
    public partial class MReadNumber : ReactiveObject
    {
        [Reactive]
        public partial int Number { get; set; }
        [Reactive]
        public partial string Text { get; set; }
        [Reactive]
        public partial ReadNumberType Type { get; set; }
    }
}
