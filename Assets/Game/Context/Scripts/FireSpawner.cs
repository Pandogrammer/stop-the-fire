using System.Linq;
using Game.Components.WathingFire.Scripts;
using UniRx;
using UnityEngine;
using Utils.Unity.PandoBehaviours;

namespace Game.Context.Scripts
{
    public class FireSpawner : AutoLoadMonoBehaviour
    {

        [SerializeField] private GameObject _prfFire;
        [SerializeField] private float _fireSpawnCooldown;

        private float _fireSpawnTimer;
    
        protected override void Load()
        {
            EveryUpdate
                .Do(_ => _fireSpawnTimer -= Time.deltaTime)
                .Where(_ => _fireSpawnTimer < 0)
                .Select(_ => GeneratePosition())
                .Do(SpawnFire)
                .Subscribe()
                .AddTo(_disposables);
        }

        private Vector3 GeneratePosition()
        {
            var externalFires = FindObjectsOfType<WatchingFire>().ToList();
            var randomIndex = Random.Range(0, externalFires.Count);
            return externalFires[randomIndex].transform.position;
        }

        private void SpawnFire(Vector3 position)
        {
            _fireSpawnTimer = _fireSpawnCooldown;
            Instantiate(_prfFire, position, Quaternion.identity);
        }
    }
}