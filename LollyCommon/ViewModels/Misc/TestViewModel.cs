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
    public class TestViewModel : ReactiveValidationObject
    {
        public ReactiveCommand<Unit, Unit> ExecuteCommand { get; }

        public TestViewModel()
        {
            ExecuteCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await new ZwvistaBlogCrawler().GetLangBlogPosts();
            });
        }
    }
}
