using System;
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

        private bool _burnt;
        protected override void Load()
        {
            _collision.OnTriggerEnterAsObservable()
                .Where(it => it.CompareTag("Fire"))
                .Subscribe(_ => Burn())
                .AddTo(_disposables);
        }

        private void Burn()
        {
            _burnt = true;
            _burntState.SetActive(true);
            Observable.Timer(TimeSpan.FromSeconds(1))
                .First()
                .Subscribe(_ => _collision.enabled = false);
        }

        public bool IsBurnt() => _burnt;
    }
}