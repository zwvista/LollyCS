using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;

namespace LollyCommon
{
    public partial class PatternCrawlersViewModel : ReactiveValidationObject
    {
        [Reactive]
        public partial string SelectedLang { get; set; } = null!;
        [Reactive]
        public partial string SelectedCrawler { get; set; } = null!;
        [Reactive]
        public partial List<string> Crawlers { get; set; } = null!;
        [Reactive]
        public partial string SelectedStep { get; set; } = null!;
        public ReactiveCommand<Unit, Unit> ExecuteCommand { get; }

        public PatternCrawlersViewModel()
        {
            this.WhenAnyValue(x => x.SelectedLang).Subscribe(v =>
            {
                Crawlers = v == null ? new List<string>() :
                GetType().Assembly.GetTypes()
                .Where(t => t.Namespace == "LollyCommon.Crawlers.Patterns." + v && t.IsPublic)
                .Select(t => t.Name).ToList();
            });
            this.ValidationRule(x => x.SelectedLang, v => !string.IsNullOrWhiteSpace(v), "SelectedLang must not be empty");
            this.ValidationRule(x => x.SelectedCrawler, v => !string.IsNullOrWhiteSpace(v), "SelectedCrawler must not be empty");
            this.ValidationRule(x => x.SelectedStep, v => !string.IsNullOrWhiteSpace(v), "SelectedStep must not be empty");
            ExecuteCommand = ReactiveCommand.Create(() =>
            {
                var t = GetType().Assembly.GetType($"LollyCommon.Crawlers.Patterns.{SelectedLang}.{SelectedCrawler}")!;
                var i = Activator.CreateInstance(t);
                var m = t.GetMethod(SelectedStep);
                m.Invoke(i, new object[] {});
            }, this.IsValid());
        }
    }
}
