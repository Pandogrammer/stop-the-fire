namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public static class LevelGenerator
    {
        public static LevelBlueprint Create(int randomSeed)
        {
            return new LevelBlueprint(randomSeed);
        }

        public static LevelBlueprint WithWidth(this LevelBlueprint levelBlueprint, int width)
        {
            levelBlueprint.SetWidth(width);
            return levelBlueprint;
        }

        public static LevelBlueprint WithHeight(this LevelBlueprint levelBlueprint, int height)
        {
            levelBlueprint.SetHeight(height);
            return levelBlueprint;
        }

        public static LevelBlueprint WithCells(this LevelBlueprint levelBlueprint)
        {
            levelBlueprint.GenerateCells();
            return levelBlueprint;
        }

        public static LevelBlueprint WithStartingPosition(this LevelBlueprint levelBlueprint,
            Position? position = null)
        {
            levelBlueprint.GenerateStartingPosition(position);
            return levelBlueprint;
        }

        public static LevelBlueprint WithBorderWalls(this LevelBlueprint levelBlueprint, bool withBorderWalls = true)
        {
            if(withBorderWalls)
                levelBlueprint.GenerateBorderWalls();
            return levelBlueprint;
        }

        public static LevelBlueprint WithCriticalPath(this LevelBlueprint levelBlueprint, int criticalPathLength,
            int newWalkerGeneration, int diminishingPathLength)
        {
            levelBlueprint.GenerateCriticalPath(criticalPathLength, newWalkerGeneration, diminishingPathLength);
            return levelBlueprint;
        }

        public static LevelBlueprint WithStairs(this LevelBlueprint levelBlueprint)
        {
            levelBlueprint.GenerateStairs();
            return levelBlueprint;
        }

        public static LevelBlueprint WithRandomWalls(this LevelBlueprint levelBlueprint, int randomWalls,
            int chunkChance, int maxChunkSize)
        {
            levelBlueprint.GenerateRandomWalls(randomWalls, chunkChance, maxChunkSize);
            return levelBlueprint;
        }
    }

    public class LevelBlueprint
    {
        private Level level;

        public LevelBlueprint(int randomSeed)
        {
            level = new Level(randomSeed);
        }

        public void SetWidth(int width)
        {
            level.width = width;
        }

        public void SetHeight(int height)
        {
            level.height = height;
        }

        public void GenerateCells()
        {
            level.cells = new Cell[level.width, level.height];
        }

        public void GenerateStartingPosition(Position? position = null)
        {
            if (position.HasValue)
                level.startingPosition = position.Value;
            else
                level.startingPosition = new Position(
                    level.random.Next(1, level.width - 1),
                    level.random.Next(1, level.height - 1));
        }

        public void GenerateBorderWalls()
        {
            level.cells = WallsGenerator.GenerateBorderWalls(level.width, level.height, level.cells);
        }

        public void GenerateCriticalPath(int criticalPathLength, int newWalkerGeneration, int diminishingPathLength)
        {
            level.criticalPath =
                CriticalPathGenerator.GenerateCriticalPath(
                    level.startingPosition,
                    criticalPathLength,
                    level.width,
                    level.height,
                    level.random,
                    newWalkerGeneration,
                    diminishingPathLength);
        }

        public void GenerateStairs()
        {
            (level.cells, level.stairsPosition) = StairsGenerator.GenerateStairs(
                level.criticalPath, level.criticalPathLength, level.startingPosition, level.cells);
        }

        public void GenerateRandomWalls(int randomWalls, int chunkChance, int maxChunkSize)
        {
            level.cells = WallsGenerator.GenerateRandomWalls(level.criticalPath, level.cells,
                randomWalls, level.random, level.width, level.height,
                chunkChance,
                maxChunkSize);
        }

        public Level Generate()
        {
            return level;
        }
    }
}