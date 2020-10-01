using UnityEngine;

namespace Utils.Unity
{
    public static class SphereCollisionChecker
    {
        public static bool Check(UnityEngine.Vector3 detectionAreaCenter, float detectionAreaRadius, string tag)
        {
            Collider[] colliders = new Collider[10];
            int size = Physics.OverlapSphereNonAlloc(detectionAreaCenter, detectionAreaRadius, colliders);
            
            for (int i = 0; i < size; i++)
            {
                var collider = colliders[i];
                if (collider.CompareTag(tag))
                    return true;
            }

            return false;
        }
    }
}