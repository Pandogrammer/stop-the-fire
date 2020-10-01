using System;
using System.Collections.Generic;
using System.Linq;
using Game.Components.Humedales.Scripts;
using UniRx;
using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.CircleOfFire.Scripts
{
    [Serializable]
    public class HumedalCircle
    {
        public int Radius;
        public GameObject CircleOfHumedales;
        public List<Humedal> Humedales => CircleOfHumedales.GetComponentsInChildren<Humedal>().ToList();
    }
    public class CircleOfFire : AutoLoadMonoBehaviour
    {
        [SerializeField] private SphereCollider _collision;
        [SerializeField] private List<HumedalCircle> _humedalCircles;

        private int state;
        protected override void Load()
        {
            SetRadius();

            EveryUpdate
                .TakeWhile(_ => StateIsInRange)
                .Where(_ => AllHumedalsOfRingAreDead(state))
                .Do(_ => AdvanceState())
                .Where(_ => StateIsInRange)
                .Do(_ => SetRadius())
                .Subscribe()
                .AddTo(_disposables);
        }

        private bool StateIsInRange => state < _humedalCircles.Count;

        private void AdvanceState() => state += 1;

        private void SetRadius()
        {
            _collision.radius = _humedalCircles[state].Radius;
        }

        private bool AllHumedalsOfRingAreDead(int ring)
        {
            return _humedalCircles[ring].Humedales.All(it => it.IsBurnt());
        }
    }
}