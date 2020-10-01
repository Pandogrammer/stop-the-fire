using System;
using System.Collections.Generic;
using System.Linq;
using Game.Components.Humedales.Scripts;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils.Extensions;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.Fire.Scripts
{
    public class Fire : AutoLoadMonoBehaviour
    {
        [SerializeField] private Collider _waterCollision;
        [SerializeField] private Collider _humedalCollision;
        [SerializeField] private float _speed;
        [SerializeField] private float _pushBackSpeed;

        private List<Humedal> _humedales;
        private float _beingKnockedBackTimer;
        protected override void Load()
        {
            _humedales = FindObjectsOfType<Humedal>().ToList();
            PushBackByWaterSubscription();
            GoTowardsNearestHumedalSubscription();
            DestroyWhenHumedalTouchedSubscription();
        }

        private void DestroyWhenHumedalTouchedSubscription()
        {
            _humedalCollision.OnTriggerEnterAsObservable()
                .Where(it => it.CompareTag("Humedal"))
                .Delay(TimeSpan.FromMilliseconds(500))
                .Subscribe(_ => Destroy(this))
                .AddTo(_disposables);
        }

        private void GoTowardsNearestHumedalSubscription()
        {
            EveryUpdate
                .Where(_ => _beingKnockedBackTimer <= 0)
                .Select(_ => FindClosestHumedal())
                .Subscribe(MoveTowardsTarget)
                .AddTo(_disposables);

            EveryUpdate
                .Where(_ => _beingKnockedBackTimer > 0)
                .Do(_ => _beingKnockedBackTimer -= Time.deltaTime)
                .Subscribe()
                .AddTo(_disposables);
        }

        private void PushBackByWaterSubscription()
        {
            _waterCollision.OnParticleCollisionAsObservable()
                .Where(it => it.CompareTag("Water"))
                .Do(_ => _beingKnockedBackTimer = 1f)
                .Subscribe(_ => PushBack())
                .AddTo(_disposables);
        }

        private void MoveTowardsTarget(Humedal target)
        {
            transform.LookAt(target.transform.position);
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        private Humedal FindClosestHumedal()
        {
            var minDistance = 10000f;
            Humedal target = null;
            _humedales.ForEach(it =>
            {
                var distance = Vector3.Distance(transform.position, it.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = it;
                }
            });
            return target;
        }

        private void PushBack()
        {
            transform.Translate(-Vector3.forward * (_pushBackSpeed * Time.deltaTime));
        }
    }
}
