using Game;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Unity.PandoBehaviours
{
    public abstract class PandoBehaviour : MonoBehaviour
    {
        protected static T Instantiate<T>(
            T original, UnityEngine.Vector3? position = null, Quaternion? rotation = null) where T : Object
        {
            T obj = position == null
                ? Object.Instantiate(original)
                : Object.Instantiate(original, position.Value, rotation ?? Quaternion.identity);
            return obj;
        }

        protected static void Destroy<T>(T unityObject, float time = 0) where T : MonoBehaviour
        {
            Object.Destroy(unityObject.gameObject);
        }
        
        protected static void Destroy(GameObject gameObject, float time = 0)
        {
            Object.Destroy(gameObject);
        }

    }
}