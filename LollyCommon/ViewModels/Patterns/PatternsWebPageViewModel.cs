using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public partial class PatternsWebPageViewModel : ReactiveObject
    {
        public List<MPattern> Patterns { get; }
        [Reactive]
        public partial int SelectedPatternIndex { get; set; }
        [ObservableAsProperty]
        public partial string URL { get; }

        public PatternsWebPageViewModel(List<MPattern> patterns, int index)
        {
            Patterns = patterns;
            SelectedPatternIndex = index;
            this.WhenAnyValue(x => x.SelectedPatternIndex, (int v) => Patterns[v].URL)
                .ToProperty(this, x => x.URL);
        }

        public void Next(int delta) =>
            SelectedPatternIndex = (SelectedPatternIndex + delta + Patterns.Count) % Patterns.Count;
    }
}
