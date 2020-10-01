using System;
using System.Collections.Generic;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public static class WallsGenerator
    {
        public static Cell[,] GenerateRandomWalls(
            List<Position> criticalPath, Cell[,] cells, int randomWalls,
            Random random, int width, int height, float chunkChance, int maxChunkSize)
        {
            for (int i = 0; i < randomWalls; i++)
            {
                var x = random.Next(1, width - 1);
                var y = random.Next(1, height - 1);
                var position = new Position(x, y);
                var generateChunk = random.Next(1, 101) >= chunkChance;

                if (generateChunk)
                {
                    var chunkSize = random.Next(1, maxChunkSize);
                    GenerateChunkOfWalls(criticalPath, cells, position, chunkSize, width, height);
                }

                TryToGenerateWall(criticalPath, cells, position, width, height);
            }

            return cells;
        }

        private static void GenerateChunkOfWalls(
            List<Position> criticalPath, Cell[,] cells, Position position,
            int chunkSize, int width, int height)
        {
            if (chunkSize == 0)
            {
                TryToGenerateWall(criticalPath, cells, position, width, height);
            }
            else
            {
                GenerateChunkOfWalls(criticalPath, cells,
                    position + new Position(0, 1),
                    chunkSize - 1, width, height);

                GenerateChunkOfWalls(criticalPath, cells,
                    position + new Position(0, -1),
                    chunkSize - 1, width, height);

                GenerateChunkOfWalls(criticalPath, cells,
                    position + new Position(1, 0),
                    chunkSize - 1, width, height);

                GenerateChunkOfWalls(criticalPath, cells,
                    position + new Position(-1, 0),
                    chunkSize - 1, width, height);
            }
        }

        private static void TryToGenerateWall(
            List<Position> criticalPath, Cell[,] cells,
            Position position, int width, int height)
        {
            if (!OutOfBounds(width, height, position) && !criticalPath.Contains(position))
                cells[position.x, position.y] = Cell.Wall;
        }


        private static bool OutOfBounds(int width, int height, Position newPosition)
        {
            return newPosition.x <= 0 || newPosition.x >= width - 1 ||
                   newPosition.y <= 0 || newPosition.y >= height - 1;
        }

        public static Cell[,] GenerateBorderWalls(int width, int height, Cell[,] cells)
        {
            for (int i = 0; i < width; i++)
            {
                cells[i, 0] = Cell.Wall;
                cells[i, height - 1] = Cell.Wall;
            }

            for (int i = 0; i < height; i++)
            {
                cells[0, i] = Cell.Wall;
                cells[width - 1, i] = Cell.Wall;
            }

            return cells;
        }
    }
}