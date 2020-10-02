using Game.Components.Humedales.Scripts;
using UniRx;
using UnityEngine;
using Utils;
using Utils.Unity.PandoBehaviours;

namespace Game.Context.Scripts
{
    public class DefeatChecker : AutoLoadMonoBehaviour
    {
        [SerializeField] private Humedal _lastHumedal;

        protected override void Load()
        {
            EveryUpdate
                .Where(_ => _lastHumedal.IsBurnt())
                .First()
                .Subscribe(_ => EventStream.Send(Events.Defeat));
        }
    }
}
