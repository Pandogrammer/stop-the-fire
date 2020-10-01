using System;
using UniRx;

namespace Utils.Clock
{
    public class DateBasedClock : IClock
    {
        private static float _lastDeltaTime;
        private static DateTimeOffset _lastTimeOffset = DateTimeOffset.UtcNow;
        public float DeltaTime() => _lastDeltaTime;
        
        static DateBasedClock()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => CalculateDeltaTime());
        }

        private static void CalculateDeltaTime()
        {
            var now = DateTimeOffset.UtcNow;
            _lastDeltaTime = (float) (now.ToUnixTimeMilliseconds() - _lastTimeOffset.ToUnixTimeMilliseconds()) / 1000;
            _lastTimeOffset = now;
        }

    }
}