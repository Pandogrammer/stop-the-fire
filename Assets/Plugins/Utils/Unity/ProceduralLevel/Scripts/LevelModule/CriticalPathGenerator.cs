using System;
using System.Collections.Generic;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public static class CriticalPathGenerator
    {
        public static List<Position> GenerateCriticalPath(Position startingPoint, int criticalPathLength, int width,
            int height, Random random, int newWalkerGeneration, int diminishingPathLength)
        {
            var criticalPath = new List<Position>();
            criticalPath.Add(startingPoint);
            GenerateWalker(startingPoint, criticalPathLength, width, height, random, criticalPath, newWalkerGeneration,
                diminishingPathLength);

            return criticalPath;
        }

        private static void GenerateWalker(Position startingPoint, int criticalPathLength, int width, int height,
            Random random,
            List<Position> criticalPath, int newWalkerGeneration, int diminishingPathLength)
        {
            var lastPositionIndex = 1;
            var goFurther = random.Next(0, 2) == 1;

            for (var i = 0; i < criticalPathLength; i++)
            {
                var lastPosition = criticalPath[criticalPath.Count - lastPositionIndex];
                if (IsSurrounded(lastPosition, criticalPath, width, height))
                {
                    lastPositionIndex++;
                    continue;
                }

                var newPosition = GenerateNewPosition(random, lastPosition);

                if (AlreadyInPath(criticalPath, newPosition)
                    || OutOfBounds(width, height, newPosition)
                    || IsNotFurther(goFurther, startingPoint, newPosition, lastPosition))
                    continue;

                criticalPath.Add(newPosition);

                lastPositionIndex = 1;
                if (i > newWalkerGeneration)
                {
                    GenerateWalker(startingPoint, criticalPathLength - diminishingPathLength, width, height, random, criticalPath,
                        newWalkerGeneration, diminishingPathLength);
                }
            }
        }

        private static bool IsSurrounded(Position lastPosition, List<Position> criticalPath, int width, int height)
        {
            var up = new Position(lastPosition.x, lastPosition.y + 1);
            var down = new Position(lastPosition.x, lastPosition.y - 1);
            var left = new Position(lastPosition.x - 1, lastPosition.y);
            var right = new Position(lastPosition.x + 1, lastPosition.y);

            return (OutOfBounds(width, height, up) || AlreadyInPath(criticalPath, up)) &&
                   (OutOfBounds(width, height, down) || AlreadyInPath(criticalPath, down)) &&
                   (OutOfBounds(width, height, left) || AlreadyInPath(criticalPath, left)) &&
                   (OutOfBounds(width, height, right) || AlreadyInPath(criticalPath, right));
        }


        private static bool AlreadyInPath(List<Position> criticalPath, Position newPosition)
        {
            return criticalPath.Contains(newPosition);
        }

        private static bool IsNotFurther(bool goFurther, Position startingPoint,
            Position newPosition, Position lastPosition)
        {
            return goFurther &&
                   Position.Distance(newPosition, startingPoint) < Position.Distance(lastPosition, startingPoint);
        }

        private static bool OutOfBounds(int width, int height, Position newPosition)
        {
            return newPosition.x == 0 || newPosition.x == width - 1 ||
                   newPosition.y == 0 || newPosition.y == height - 1;
        }

        private static Position GenerateNewPosition(Random random, Position lastPosition)
        {
            var direction = random.Next(0, 4);
            var x = lastPosition.x;
            var y = lastPosition.y;
            switch (direction)
            {
                case 0:
                    x -= 1;
                    break;
                case 1:
                    x += 1;
                    break;
                case 2:
                    y -= 1;
                    break;
                case 3:
                    y += 1;
                    break;
            }

            return new Position(x, y);
        }
    }
}