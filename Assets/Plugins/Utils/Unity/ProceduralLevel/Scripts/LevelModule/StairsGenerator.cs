using System.Collections.Generic;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public static class StairsGenerator
    {
        public static (Cell[,] cells, Position stairsPosition) GenerateStairs(List<Position> criticalPath, int criticalPathLength, Position startingPosition, Cell[,] cells)
        {
            var tries = criticalPath.Count / 2;
            var desiredDistance = criticalPathLength / 3;
            var i = 0;
            var done = false;
            var maxDistance = 0F;
            var maxDistanceIndex = 0;
            Position stairsPosition = new Position(0,0);
            
            while (i < tries && !done)
            {
                var positionIndex = criticalPath.Count - 1 - i;
                stairsPosition = criticalPath[positionIndex];
                var distance = Position.Distance(startingPosition, stairsPosition);
                if (distance >= desiredDistance)
                {
                    cells[stairsPosition.x, stairsPosition.y] = Cell.Stairs;
                    done = true;
                }
                else
                {
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        maxDistanceIndex = positionIndex;
                    }
                }

                i++;
            }

            if (!done)
            {
                stairsPosition = criticalPath[maxDistanceIndex];
                cells[stairsPosition.x, stairsPosition.y] = Cell.Stairs;
            }

            return (cells, stairsPosition);
        }
    }
}