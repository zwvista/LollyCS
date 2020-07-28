using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class TransformSourceViewModel : ReactiveObject
    {
        [Reactive]
        public string Text { get; set; }
        [Reactive]
        public string URL { get; set; }
    }
}
