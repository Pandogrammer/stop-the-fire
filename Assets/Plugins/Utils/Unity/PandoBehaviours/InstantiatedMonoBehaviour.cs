using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Unity.PandoBehaviours
{
    public abstract class InstantiatedMonoBehaviour : PandoBehaviour
    {
        public static T InternalInstantiate<T>(T original, UnityEngine.Vector3? position = null, Quaternion? rotation = null)
            where T : PandoBehaviour
        {
            T obj;
            if (position != null && rotation != null)
                obj = Object.Instantiate(original, position.Value, rotation.Value);
            else 
                obj = Object.Instantiate(original);
            return obj;
        }

        public static void InternalDestroy<T>(T original, float time) where T : Object
        {
            var casted = original as PandoBehaviour;
            if (casted == null)
                return;
            Object.Destroy(casted.gameObject, time);
        }
    }
}