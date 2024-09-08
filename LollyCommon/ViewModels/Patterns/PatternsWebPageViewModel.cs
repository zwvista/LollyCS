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
        public int CurrentPatternIndex { get; set; }
        public string URL { [ObservableAsProperty] get; }

        public PatternsWebPageViewModel(List<MPattern> patterns, int index)
        {
            Patterns = patterns;
            CurrentPatternIndex = index;
            this.WhenAnyValue(x => x.CurrentPatternIndex, (int v) => Patterns[v].URL)
                .ToPropertyEx(this, x => x.URL);
        }

        public void Next(int delta) =>
            CurrentPatternIndex = (CurrentPatternIndex + delta + Patterns.Count) % Patterns.Count;
    }
}
