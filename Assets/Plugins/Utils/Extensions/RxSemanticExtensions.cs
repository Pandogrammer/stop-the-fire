using System;
using UniRx;

namespace Utils.Extensions
{
    public static class RxSemanticExtensions
    {
        /**
         * Espera X tiempo antes de empezar a recibir (tarda en suscribirse)
         */
        public static IObservable<T> InitialCooldown<T>(this IObservable<T> source, TimeSpan dueTime)
        {
            return source.DelaySubscription(dueTime);
        }
        
        /**
         * Espera X segundos antes de empezar a recibir (tarda en suscribirse)
         */
        public static IObservable<T> InitialCooldown<T>(this IObservable<T> source, float seconds)
        {
            return source.InitialCooldown(TimeSpan.FromSeconds(seconds));
        }
        
        /**
         * Recibe el primer elemento y luego tarda X tiempo en poder volver a recibir otro
         */
        public static IObservable<TSource> Cooldown<TSource>(this IObservable<TSource> source, TimeSpan dueTime)
        {
            return source.ThrottleFirst(dueTime);
        }
        
        /**
         * Recibe el primer elemento y luego tarda X segundos en poder volver a recibir otro
         */
        public static IObservable<TSource> Cooldown<TSource>(this IObservable<TSource> source, float seconds)
        {
            return source.Cooldown(TimeSpan.FromSeconds(seconds));
        }
        

        /**
         * Tarda X tiempo en ejecutar la siguiente sentencia
         */
        public static IObservable<T> Wait<T>(this IObservable<T> source, TimeSpan dueTime)
        {
            return source.Delay(dueTime);
        }
        
        /**
         * Tarda X segundos en ejecutar la siguiente sentencia
         */
        public static IObservable<T> Wait<T>(this IObservable<T> source, float seconds)
        {
            return source.Wait(TimeSpan.FromSeconds(seconds));
        }
    }
}