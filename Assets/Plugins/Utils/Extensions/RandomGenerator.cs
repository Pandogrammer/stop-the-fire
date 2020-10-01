using UnityEngine;

namespace Utils.Extensions
{
    public static class RandomGenerator
    {
        public static T ThisOrThat<T>(T one, T other)
        {
            return Random.Range(0, 2).ToBool() ? one : other;
        }
    }
}