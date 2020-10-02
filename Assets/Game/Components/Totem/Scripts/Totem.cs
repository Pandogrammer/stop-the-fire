using Game.Context.Scripts;
using UniRx;
using UnityEngine;
using Utils;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.Totem.Scripts
{
    public class Totem : AutoLoadMonoBehaviour
    {
        [SerializeField] private ParticleSystem _waterGun;
        private ParticleSystem.EmissionModule _waterEmission;
        private float _stopShootingTimer;

        public void Shoot()
        {
            _stopShootingTimer = 0.1f;
            _waterEmission.enabled = true;

        }

        protected override void Load()
        {
            _waterEmission = _waterGun.emission;
            _waterEmission.enabled = false;
            StopShootingSubscription();
        }


        private void StopShootingSubscription()
        {
            EveryUpdate
                .Do(_ => _stopShootingTimer -= Time.deltaTime)
                .Where(_ => _stopShootingTimer < 0)
                .Subscribe(_ => _waterEmission.enabled = false)
                .AddTo(_disposables);
        }
    }
}