using System;
using UniRx;
using UnityEngine;

namespace Utils.Unity
{
    public class RandomMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float directionChangeCooldown;
        private CompositeDisposable _disposables = new CompositeDisposable();
        private float directionChangeTimer;
        private float randomX;
        private float randomZ;

        private void OnEnable() => StartMoving();
        private void OnDisable() => StopMoving();


        private void StartMoving()
        {
            UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                var deltaTime = Time.deltaTime;
                CheckDirectionChange(deltaTime);
                Move(deltaTime);
            }).AddTo(_disposables);
        }

        private void StopMoving()
        {
            _disposables.Clear();
        }

        private void Move(float deltaTime)
        {
            var transform1 = transform;
            var x = (transform1.right * randomX).x * deltaTime * speed;
            var z = (transform1.forward * randomZ).z * deltaTime * speed;
            transform.Translate(x, transform1.position.y, z);
        }

        private void CheckDirectionChange(float deltaTime)
        {
            if (directionChangeTimer > 0)
                directionChangeTimer -= deltaTime;

            if (directionChangeTimer > 0)
                return;

            directionChangeTimer = directionChangeCooldown;
            randomX = UnityEngine.Random.Range(-2, 2);
            randomZ = UnityEngine.Random.Range(-2, 2);
        }
    }
}