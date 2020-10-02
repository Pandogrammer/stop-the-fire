using System.Collections.Generic;
using System.Linq;
using Game.Components.Humedales.Scripts;
using UniRx;
using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.Animals
{
    public class Animal : AutoLoadMonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int JumpAnimation = Animator.StringToHash("Jump");

        private List<Humedal> _humedales;
        private Humedal _actualHumedal;

        private Vector3 _jumpAroundPosition;
        
        protected override void Load()
        {
            _humedales = FindObjectsOfType<Humedal>().ToList();

            _actualHumedal = FindClosestAliveHumedal();
            transform.position = _actualHumedal.transform.position;

            EveryUpdate
                .Where(_ => !_actualHumedal.IsBurnt())
                .Subscribe(_ => JumpAroundHumedal())
                .AddTo(_disposables);
            
            EveryUpdate
                .Where(_ => _actualHumedal.IsBurnt())
                .Subscribe(_ => GoToOtherHumedal())
                .AddTo(_disposables);
        }

        private void JumpAroundHumedal()
        {
            var distance = Vector3.Distance(transform.position, _jumpAroundPosition);
            if (distance > -1)
            {
                Jump(true);                
            }
            else{
                var randomDistance = new Vector3(Random.Range(0, 3f), 0, Random.Range(0, 3f));
                _jumpAroundPosition = _actualHumedal.transform.position + randomDistance;
            }
        }

        private void GoToOtherHumedal()
        {
            _actualHumedal = FindClosestAliveHumedal();
            transform.position = _actualHumedal.transform.position;
        }


        private Humedal FindClosestAliveHumedal()
        {
            var minDistance = 10000f;
            Humedal target = null;
            _humedales.ForEach(it =>
            {
                if (it.IsBurnt())
                    return;
                var distance = Vector3.Distance(transform.position, it.transform.position);
                if (!(distance < minDistance)) 
                    return;
                minDistance = distance;
                target = it;
            });
            return target;
        }
        
        private void Jump(bool value)
        {
            _animator.SetBool(JumpAnimation, value);
        }
    }
}
