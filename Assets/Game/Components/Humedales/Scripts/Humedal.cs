using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.Humedales.Scripts
{
    public class Humedal : AutoLoadMonoBehaviour
    {
        [SerializeField] private Collider _collision;
        [SerializeField] private GameObject _burntState;
        [SerializeField] private int _ringFromCenter;

        private bool _burnt;
        public int RingFromCenter => _ringFromCenter;
        protected override void Load()
        {
            _collision.OnTriggerEnterAsObservable()
                .Where(it => it.CompareTag("Fire"))
                .Subscribe(_ => Burn());
        }

        private void Burn()
        {
            _burnt = true;
            _burntState.SetActive(true);
        }

        public bool IsBurnt() => _burnt;
    }
}