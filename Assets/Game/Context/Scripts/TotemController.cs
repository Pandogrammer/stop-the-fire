using Game.Components.Player;
using UniRx;
using UnityEngine;
using Utils.Unity.PandoBehaviours;
using TotemEntity = Game.Components.Totem.Scripts.Totem;

namespace Game.Context.Scripts
{
    public class TotemController : AutoLoadMonoBehaviour
    {
        [SerializeField] private CaptureStickMovement _captureStickMovement;
        [SerializeField] private TotemEntity _totem;

        protected override void Load()
        {
            _captureStickMovement
                .OnPlayerMovement
                .Subscribe(RotateAndShootTowardsMouse);
        }

        private void RotateAndShootTowardsMouse(Vector3 direction)
        {
            _totem.transform.LookAt(direction);
            _totem.Shoot();
        }
    }
}
