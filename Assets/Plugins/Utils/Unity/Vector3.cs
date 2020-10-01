using System;

namespace Utils.Unity
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public static Vector3 From(UnityEngine.Vector3 position)
        {
            var x = (float) Math.Round(position.x, 2);
            var y = (float) Math.Round(position.y, 2);
            var z = (float) Math.Round(position.z, 2);
            return new Vector3 {x = x, y = y, z = z};
        }

        public override string ToString()
        {
            return $"(x: {x}, y: {y}, z: {z})";
        }

        public UnityEngine.Vector3 ToVector3()
        {
            return new UnityEngine.Vector3(x, y, z);
        }

        public static Vector3 Create(float x = 0, float y = 0, float z = 0)
        {
            return new Vector3 {x = x, y = y, z = z};
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3 {x = a.x + b.x, y = a.y + b.y, z = a.z + b.z};
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3 {x = a.x - b.x, y = a.y - b.y, z = a.z - b.z};
        }
        
        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3{x = a.x * d,y = a.y * d,z = a.z * d};
        }
    }
}