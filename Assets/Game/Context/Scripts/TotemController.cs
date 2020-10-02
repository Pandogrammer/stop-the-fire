using Game.Components.Player;
using UniRx;
using UnityEngine;
using Utils;
using Utils.Unity.PandoBehaviours;
using TotemEntity = Game.Components.Totem.Scripts.Totem;

namespace Game.Context.Scripts
{
    public class TotemController : AutoLoadMonoBehaviour
    {
        [SerializeField] private CaptureStickMovement _captureStickMovement;
        [SerializeField] private TotemEntity _totem;
        private bool playing = true;

        protected override void Load()
        {
            _captureStickMovement
                .OnPlayerMovement
                .Where(_ => playing)
                .Subscribe(RotateAndShootTowardsMouse);
            
            StopShootingOnDefeatSubscription();
        }
        
        private void StopShootingOnDefeatSubscription()
        {
            EventStream.Receive<Defeat>()
                .Subscribe(_ => playing = false)
                .AddTo(_disposables);
        }

        private void RotateAndShootTowardsMouse(Vector3 direction)
        {
            _totem.transform.LookAt(direction);
            _totem.Shoot();
        }
    }
}
