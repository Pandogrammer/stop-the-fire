using System;
using UniRx;

namespace Utils.Unity.PandoBehaviours
{
    public abstract class UpdateableMonoBehaviour : PandoBehaviour
    {
        private static IObservable<Unit> _everyUpdate;

        protected static IObservable<Unit> EveryUpdate =>
            _everyUpdate ?? (_everyUpdate = Observable.EveryUpdate().AsUnitObservable());
    }
}