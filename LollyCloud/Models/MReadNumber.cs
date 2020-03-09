using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MReadNumber : ReactiveObject
    {
        [Reactive]
        public int Number { get; set; }
        [Reactive]
        public string Text { get; set; }
    }
}
