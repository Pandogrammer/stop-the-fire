using UniRx;
using UnityEngine;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private UnityLevelGenerator levelGenerator;
        [SerializeField] private LevelSpawner levelSpawner;
        
        [SerializeField] private bool _listenToUserInput;
        [SerializeField] private bool _useStartingPosition;
        [SerializeField] private Transform _startingPosition;
        public Level Level { get; private set; }

        public void Load()
        {
            var position = GetStartingPosition();
            GenerateWorld(position);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (!_listenToUserInput)
                    return;
                LevelRegeneration();
            });
        }

        private Position? GetStartingPosition()
        {
            if (!_useStartingPosition)
                return null;
            var position = _startingPosition.localPosition;
            return new Position((int) position.x, (int) position.z);
        }

        private void GenerateWorld(Position? startingPosition = null)
        {
            Level = levelGenerator.GenerateLevel(startingPosition);
            levelSpawner.SpawnLevel(Level);
        }

        private void LevelRegeneration()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                levelGenerator.ChangeSeed(-1);
                RegenerateWorld(GetStartingPosition());
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                levelGenerator.ChangeSeed(1);
                RegenerateWorld(GetStartingPosition());
            }
        }

        private void RegenerateWorld(Position? startingPosition = null)
        {
            levelSpawner.DestroyLevel();
            GenerateWorld(startingPosition);
        }
    }
}
