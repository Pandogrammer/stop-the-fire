using System.Collections.Generic;
using System.Linq;
using Game.Components.Humedales.Scripts;
using UniRx;
using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.CircleOfFire.Scripts
{
    public class CircleOfFire : AutoLoadMonoBehaviour
    {
        [SerializeField] private SphereCollider _collision;

        private List<int> _radius = new List<int> {10, 7, 3};
        private List<List<Humedal>> _humedalCircles = new List<List<Humedal>>();

        private int state;
        protected override void Load()
        {
            Initialize();

            EveryUpdate
                .Where(_ => AllHumedalsOfRingAreDead(state))
                .Subscribe(_ => AdvanceState())
                .AddTo(_disposables);
        }

        private void AdvanceState()
        {
            state += 1;
            _collision.radius = _radius[state];
        }

        private bool AllHumedalsOfRingAreDead(int ring)
        {
            return _humedalCircles[ring].All(it => it.IsBurnt());
        }

        private void Initialize()
        {
            for (var i = 0; i < _radius.Count; i++)
            {
                if (_humedalCircles[i] == null)
                    _humedalCircles[i] = new List<Humedal>();
            }

            var humedales = FindObjectsOfType<Humedal>().ToList();
            humedales.ForEach(it => { _humedalCircles[it.RingFromCenter].Add(it); });

            _collision.radius = _radius[state];
        }
    }
}