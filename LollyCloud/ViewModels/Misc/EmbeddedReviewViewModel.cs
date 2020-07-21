using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace LollyCloud
{
    public class EmbeddedReviewViewModel : ReactiveObject
    {
        public MReviewOptions Options { get; } = new MReviewOptions();
        IDisposable subscriptionTimer;
        public bool IsRunning => subscriptionTimer != null;

        public void Stop()
        {
            subscriptionTimer?.Dispose();
            subscriptionTimer = null;
        }

        public void Start(List<int> ids, Action<int> getOne)
        {
            subscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(i =>
                {
                    if (i < ids.Count)
                        getOne(ids[(int)i]);
                    else
                        Stop();
                });
        }
    }
}
