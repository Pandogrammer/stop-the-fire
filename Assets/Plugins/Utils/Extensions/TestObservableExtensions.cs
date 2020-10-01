using System;
using UniRx;

namespace Utils.Extensions
{
    public static class TestObservableExtensions
    {
        private static IScheduler _scheduler = Scheduler.Immediate;
        
        public static void Test(this IObservable<Unit> execute)
        {
            execute.SubscribeOn(_scheduler).Subscribe();
        }
        public static T Test<T>(this IObservable<T> execute)
        {
            T value = default;
            execute.SubscribeOn(_scheduler).Subscribe(it => value = it);
            return value;
        }
    }

    
}