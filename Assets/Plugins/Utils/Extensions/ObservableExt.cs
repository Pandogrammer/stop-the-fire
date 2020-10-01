using System;
using UniRx;

namespace Utils.Extensions
{
    public static class ObservableExt
    {
        public static IObservable<Unit> AsUnit(Action action)
        {
            return action.AsUnitObservable();
        }

        public static IObservable<Unit> AsUnitObservable(this Action action)
        {
            return Observable.Create<Unit>(e =>
            {
                action();
                e.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}