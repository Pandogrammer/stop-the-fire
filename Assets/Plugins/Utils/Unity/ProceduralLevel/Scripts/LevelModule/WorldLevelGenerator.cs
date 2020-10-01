using UnityEngine;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public class WorldLevelGenerator : UnityLevelGenerator
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int criticalPathLength;
        [SerializeField] private int randomWalls;
        [SerializeField] private int wallChunkChancePercentage;
        [SerializeField] private int maxChunkSize;
        [SerializeField] private int randomSeed;
        [SerializeField] private int walkerStepsForNewWalkerSpawn;
        [SerializeField] private int diminishingPathLength;

        public override Level GenerateLevel(Position? startingPosition)
        {
            var level = LevelGenerator
                .Create(randomSeed)
                .WithHeight(height)
                .WithWidth(width)
                .WithCells()
                .WithBorderWalls()
                .WithStartingPosition(startingPosition)
                .WithCriticalPath(criticalPathLength, walkerStepsForNewWalkerSpawn, diminishingPathLength)
                .WithStairs()
                .WithRandomWalls(randomWalls, wallChunkChancePercentage, maxChunkSize)
                .Generate();

            return level;
        }

        public override void ChangeSeed(int value)
        {
            randomSeed += value;
        }

        public override void AdvanceLevel()
        {
            randomSeed += 1;
            height += 3;
            width += 3;
            criticalPathLength += 5;
            walkerStepsForNewWalkerSpawn += 2;
            diminishingPathLength += 2;
            randomWalls += 20;
        }
    }

}