using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace LollyCommon
{
    public class TestViewModel : ReactiveObject
    {
        [Reactive]
        public string SelectedLang { get; set; }
        [Reactive]
        public string SelectedCrawler { get; set; }
        [Reactive]
        public List<string> Crawlers { get; set; }
        [Reactive]
        public string SelectedStep { get; set; }

        public TestViewModel()
        {
            this.WhenAnyValue(x => x.SelectedLang).Subscribe(v =>
            {
                Crawlers = string.IsNullOrEmpty(v) ? new List<string>() :
                GetType().Assembly.GetTypes()
                .Where(t => t.Namespace == "LollyCommon.Crawlers.Patterns." + v && t.IsPublic)
                .Select(t => t.Name).ToList();
            });
        }
    }
}
