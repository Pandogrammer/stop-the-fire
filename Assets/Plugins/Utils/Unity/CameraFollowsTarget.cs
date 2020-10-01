using UniRx;
using UnityEngine;

namespace Game.Components.Player
{
    public class CameraFollowsTarget : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _target;
        [Header("Joystick properties [can be null]")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _dragAmountMultiplier;
        [SerializeField] private bool _observeOnFixedUpdate;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private Vector3 _cameraDistance;

        private void Awake()
        {
            if (_camera == null) return;
            if (_target == null) return;
            Load(_camera, _target, _joystick);
        }


        public void Load(Camera camera, GameObject target, Joystick joystick = null)
        {
            _camera = camera;
            _target = target;
            _joystick = joystick;  
            _cameraDistance = _camera.transform.position;

            if(_observeOnFixedUpdate)
                Observable
                    .EveryFixedUpdate()
                    .Subscribe(_ => MoveCamera(Time.fixedDeltaTime))
                    .AddTo(_disposables);
            else
                Observable
                    .EveryUpdate()
                    .Subscribe(_ => MoveCamera(Time.deltaTime))
                    .AddTo(_disposables);
        }

        public void Unload()
        {
            _disposables.Clear();
        }

        private void MoveCamera(float deltaTime)
        {
            if (_target == null)
                return;
            var initialPosition = _camera.transform.position;
            var addedPosition = _joystick != null ? CaptureJoystickMovement() : Vector3.zero;
            var expectedPosition = _target.transform.position + _cameraDistance + addedPosition;
            _camera.transform.position = Vector3.Slerp(initialPosition, expectedPosition, deltaTime);
        }
        
        private Vector3 CaptureJoystickMovement()
        {
            var direction = _joystick.Direction;
            var rotation = _camera.transform.rotation;
            var updated = rotation * direction;
            updated.y = 0;
            return updated * _dragAmountMultiplier;
        }
    }
}