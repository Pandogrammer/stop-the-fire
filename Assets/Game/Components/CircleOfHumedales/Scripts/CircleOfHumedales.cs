using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.Components.CircleOfHumedales.Scripts
{
    public class CircleOfHumedales : AutoLoadMonoBehaviour
    {
        [SerializeField] private GameObject _aliveState;
        [SerializeField] private GameObject _burntState;
        protected override void Load()
        {
            if (_burntState == null)
                return;
            _aliveState.SetActive(true);
            _burntState.SetActive(false);
        }

        public void SetAsBurnt()
        {
            if (_burntState == null)
                return;
            _aliveState.SetActive(false);
            _burntState.SetActive(true);
        }
    }
}
