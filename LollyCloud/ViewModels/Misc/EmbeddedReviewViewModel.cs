using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class EmbeddedReviewViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        public MReviewOptions Options { get; set; } = new MReviewOptions();

        public EmbeddedReviewViewModel(SettingsViewModel vmSettings)
        {
            this.vmSettings = vmSettings;
        }

        public async Task Start(List<int> ids, int interval, Func<int, Task> getOne, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            for (int i = 0; i < ids.Count; i++)
            {
                await Task.Delay((int)interval);
                await getOne(i);
            }
        }
    }
}
