using UnityEngine;

namespace Utils.Unity
{
    public struct QuaternionParsed
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion ToQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }

        public static QuaternionParsed From(Quaternion quaternion)
        {
            return new QuaternionParsed
            {
                x = quaternion.x, y = quaternion.y, 
                z = quaternion.z, w = quaternion.w
            };
        }
    }
}