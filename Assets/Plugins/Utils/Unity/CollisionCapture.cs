using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Utils.Unity
{
    public class CollisionCapture : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        void Start()
        {
            _collider
                .OnTriggerEnterAsObservable()
                .Subscribe(Collision);
        }


        private void Collision(Collider it)
        {
            var otherTag = it.gameObject.tag;
            var thisTag = _collider.gameObject.tag;
            Debug.Log($"this: {thisTag}, other: {otherTag}");
        }

    }
}
