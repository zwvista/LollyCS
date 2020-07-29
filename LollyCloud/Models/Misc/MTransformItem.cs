using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Reactive;

namespace LollyCloud
{
    public class MTransformItem : ReactiveObject
    {
        [Reactive]
        public int Index { get; set; }
        [Reactive]
        public string Extractor { get; set; }
        [Reactive]
        public string Replacement { get; set; }
    }
    public class MTransformItemEdit : ReactiveValidationObject<MTransformItemEdit>
    {
        [Reactive]
        public int Index { get; set; }
        [Reactive]
        public string Extractor { get; set; }
        [Reactive]
        public string Replacement { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MTransformItemEdit()
        {
            this.ValidationRule(x => x.Extractor, v => !string.IsNullOrWhiteSpace(v), "Extractor must not be empty");
            this.ValidationRule(x => x.Replacement, v => !string.IsNullOrWhiteSpace(v), "Replacement must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
