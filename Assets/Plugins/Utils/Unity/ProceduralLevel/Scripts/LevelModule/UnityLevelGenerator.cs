using UnityEngine;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public abstract class UnityLevelGenerator : MonoBehaviour
    {
        public abstract Level GenerateLevel(Position? startingPosition = null);
        public abstract void ChangeSeed(int value);
        public abstract void AdvanceLevel();
    }
}