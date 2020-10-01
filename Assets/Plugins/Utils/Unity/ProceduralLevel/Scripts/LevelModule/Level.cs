using System;
using System.Collections.Generic;

namespace Utils.Unity.ProceduralLevel.Scripts.LevelModule
{
    public class Level
    {
        public int width;
        public int height;
        public Cell[,] cells;
        public Position startingPosition;
        public Position stairsPosition;
        public List<Position> criticalPath;
        public Random random;
        public int criticalPathLength;

        public Level(int randomSeed)
        {
            random = new Random(randomSeed);
        }

        public Level()
        {
            
        }


        public Cell GetCellAt(int x, int y)
        {
            return cells[x, y];
        }
    }

}