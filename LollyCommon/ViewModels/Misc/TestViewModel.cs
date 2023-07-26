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
        public ReactiveCommand<Unit, Unit> ExecuteCommand { get; }

        public TestViewModel()
        {
            ExecuteCommand = ReactiveCommand.Create(() =>
            {
            });
        }
    }
}
