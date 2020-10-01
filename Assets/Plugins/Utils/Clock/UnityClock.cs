using UniRx;
using UnityEngine;

namespace Utils.Clock
{
    public class UnityClock : IClock
    {
        private static float _lastDeltaTime;

        public float DeltaTime() => _lastDeltaTime;

        static UnityClock()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => _lastDeltaTime = Time.deltaTime);
        }
    }
}