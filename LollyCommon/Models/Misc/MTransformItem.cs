using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Reactive;

namespace LollyCommon
{
    public class MTransformItem : ReactiveObject
    {
        [Reactive]
        public int Index { get; set; }
        [Reactive]
        public string Extractor { get; set; } = "";
        [Reactive]
        public string Replacement { get; set; } = "";
    }
    public class MTransformItemEdit : ReactiveValidationObject
    {
        [Reactive]
        public int Index { get; set; }
        [Reactive]
        public string Extractor { get; set; } = "";
        [Reactive]
        public string Replacement { get; set; } = "";
        public ReactiveCommand<Unit, Unit> Save { get; }
        public MTransformItemEdit()
        {
            this.ValidationRule(x => x.Extractor, v => !string.IsNullOrWhiteSpace(v), "Extractor must not be empty");
            this.ValidationRule(x => x.Replacement, v => !string.IsNullOrWhiteSpace(v), "Replacement must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
