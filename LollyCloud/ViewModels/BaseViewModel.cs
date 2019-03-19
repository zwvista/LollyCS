using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LollyShared
{
    public class BaseViewModel : ReactiveObject
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => this.RaiseAndSetIfChanged(ref isBusy, value);
        }

        string title = string.Empty;
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
    }
}
