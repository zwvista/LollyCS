using ReactiveUI;
using ReactiveUI.Fody.Helpers;
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
    public class TestViewModel : ReactiveValidationObject
    {
        [Reactive]
        public string SelectedLang { get; set; }
        [Reactive]
        public string SelectedCrawler { get; set; }
        [Reactive]
        public List<string> Crawlers { get; set; }
        [Reactive]
        public string SelectedStep { get; set; }
        public ReactiveCommand<Unit, Unit> ExecuteCommand { get; }

        public TestViewModel()
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
                var t = GetType().Assembly.GetType($"LollyCommon.Crawlers.Patterns.{SelectedLang}.{SelectedCrawler}");
                var i = Activator.CreateInstance(t);
                var m = t.GetMethod(SelectedStep);
                m.Invoke(i, new object[] {});
            }, this.IsValid());
        }
    }
}
