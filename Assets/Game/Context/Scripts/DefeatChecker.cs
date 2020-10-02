using System.Collections.Generic;
using System.Linq;
using Game.Components.Humedales.Scripts;
using UniRx;
using UnityEngine;
using Utils;
using Utils.Unity.PandoBehaviours;

namespace Game.Context.Scripts
{
    public class DefeatChecker : AutoLoadMonoBehaviour
    {
        private List<Humedal> _allHumedals;

        protected override void Load()
        {
            _allHumedals = FindObjectsOfType<Humedal>().ToList();
            
            EveryUpdate
                .Where(_ => _allHumedals.All(it => it.IsBurnt()))
                .First()
                .Subscribe(_ => EventStream.Send(Events.Defeat()));
        }
    }
}
