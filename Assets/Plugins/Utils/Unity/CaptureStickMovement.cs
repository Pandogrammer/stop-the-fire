using System;
using UniRx;
using UnityEngine;

namespace Game.Components.Player
{
    public class CaptureStickMovement : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Camera _camera;

        private Subject<Vector3> _onPlayerMovement = new Subject<Vector3>();

        public IObservable<Vector3> OnPlayerMovement => _onPlayerMovement;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        private void Awake()
        {
            if (_joystick == null) return;
            if (_camera == null) return;
            Load(_joystick,  _camera);
        }


        public void Load(Joystick joystick, Camera camera)
        {
            _joystick = joystick;
            _camera = camera;
            
            Observable
                .EveryUpdate()
                .Select(_ => CaptureMovement())
                .Where(it => it.HasValue)
                .Select(it => it.Value)
                .Subscribe(it => _onPlayerMovement.OnNext(it))
                .AddTo(_disposables);
        }

        public void Unload()
        {
            _disposables.Clear();
        }

        private Vector3? CaptureMovement()
        {
            var direction = _joystick.Direction;
            if (direction == Vector2.zero)
                return null;
            var rotation = _camera.transform.rotation;
            var updated = rotation * direction;
            updated.y = 0;
            return updated;
        }
    }
}