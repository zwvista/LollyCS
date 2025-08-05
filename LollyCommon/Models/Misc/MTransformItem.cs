using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System.Reactive;

namespace LollyCommon
{
    public partial class MTransformItem : ReactiveObject
    {
        [Reactive]
        public partial int Index { get; set; }
        [Reactive]
        public partial string Extractor { get; set; } = "";
        [Reactive]
        public partial string Replacement { get; set; } = "";
    }
    public partial class MTransformItemEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int Index { get; set; }
        [Reactive]
        public partial string Extractor { get; set; } = "";
        [Reactive]
        public partial string Replacement { get; set; } = "";
        public ReactiveCommand<Unit, Unit> Save { get; }
        public MTransformItemEdit()
        {
            this.ValidationRule(x => x.Extractor, v => !string.IsNullOrWhiteSpace(v), "Extractor must not be empty");
            this.ValidationRule(x => x.Replacement, v => !string.IsNullOrWhiteSpace(v), "Replacement must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
