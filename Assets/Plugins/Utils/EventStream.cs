using System;
using UniRx;

namespace Utils
{
    public static class EventStream
    {
        private static MessageBroker messageBroker; //antes era en Rx puro pero bueno vamo a confiar

        static EventStream()
        {
            messageBroker = new MessageBroker();
        }

        public static IObservable<T> Receive<T>()
        {
            return messageBroker.Receive<T>();
        }

        public static void Send<T>(T message)
        {
            messageBroker.Publish(message);
        }
    }
}