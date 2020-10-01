using System.Collections.Generic;

namespace Utils
{
    public class GeneralRepository
    {
        private static Dictionary<string, object> _stuff = new Dictionary<string, object>();

        public static void Set(string key, object value)
        {
            _stuff[key] = value;
        }

        public static T Get<T>(string key)
        {
            return (T) _stuff[key];
        }
    }
}