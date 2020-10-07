using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils.Extensions;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.WathingFire.Scripts
{
    public class WatchingFire : AutoLoadMonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collision;
        private static readonly int JumpAnimation = Animator.StringToHash("Jump");
        private bool canMove = true;

        protected override void Load()
        {
            EveryUpdate
                .Subscribe(_ => transform.LookAt(Vector3.zero))
                .AddTo(_disposables);

            _collision.OnTriggerEnterAsObservable()
                .Where(it => it.CompareTag("CircleOfFire"))
                .Do(_ => canMove = false)
                .Subscribe()
                .AddTo(_disposables);

            _collision.OnTriggerExitAsObservable()
                .Where(it => it.CompareTag("CircleOfFire"))
                .Do(_ => canMove = true)
                .Subscribe()
                .AddTo(_disposables);

            EveryUpdate
                .Where(_ => canMove)
                .Subscribe(_ => transform.Translate(Vector3.forward * Time.deltaTime))
                .AddTo(_disposables);

            
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1f))
                .Do(_ => Jump(true))
                .Wait(0.5f)
                .Do(_ => Jump(false))
                .Subscribe()
                .AddTo(_disposables);
        }

        private void Jump(bool value)
        {
            _animator.SetBool(JumpAnimation, value);
        }
    }
}
