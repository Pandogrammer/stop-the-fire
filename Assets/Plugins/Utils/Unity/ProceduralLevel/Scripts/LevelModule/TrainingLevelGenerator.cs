using UnityEngine;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public class TrainingLevelGenerator : UnityLevelGenerator
    {
        [SerializeField] private int randomSeed;
        [SerializeField] private int width;
        [SerializeField] private int height;

        public override Level GenerateLevel(Position? startingPosition)
        {
            return LevelGenerator.Create(randomSeed)
                .WithWidth(width)
                .WithHeight(height)
                .WithCells()
                .WithBorderWalls()
                .WithStartingPosition(startingPosition)
                .Generate();
        }

        public override void ChangeSeed(int value)
        {
            //throw new System.NotImplementedException();
        }

        public override void AdvanceLevel()
        {
            //throw new System.NotImplementedException();
        }
    }
}