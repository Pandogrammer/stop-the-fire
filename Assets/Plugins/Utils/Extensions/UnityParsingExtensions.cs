using UnityEngine;
using Utils.Unity;
using Vector3 = Utils.Unity.Vector3;

namespace Utils.Extensions
{
    public static class UnityParsingExtensions
    {
        public static Vector3 Parse(this UnityEngine.Vector3 vector3)
        {
            return Vector3.From(vector3);
        }

        public static QuaternionParsed Parse(this Quaternion quaternion)
        {
            return QuaternionParsed.From(quaternion);
        }

    }
}