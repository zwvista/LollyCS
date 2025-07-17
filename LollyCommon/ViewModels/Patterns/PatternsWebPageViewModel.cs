using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class PatternsWebPageViewModel : ReactiveObject
    {
        public List<MPattern> Patterns { get; }
        [Reactive]
        public int SelectedPatternIndex { get; set; }
        public string URL { [ObservableAsProperty] get; }

        public PatternsWebPageViewModel(List<MPattern> patterns, int index)
        {
            Patterns = patterns;
            SelectedPatternIndex = index;
            this.WhenAnyValue(x => x.SelectedPatternIndex, (int v) => Patterns[v].URL)
                .ToPropertyEx(this, x => x.URL);
        }

        public void Next(int delta) =>
            SelectedPatternIndex = (SelectedPatternIndex + delta + Patterns.Count) % Patterns.Count;
    }
}
