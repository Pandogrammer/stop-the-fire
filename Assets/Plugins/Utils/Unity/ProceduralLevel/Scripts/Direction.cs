using System;

namespace Utils.Unity.ProceduralLevel.Scripts
{
    public enum Direction
    {
        North,
        West,
        East,
        South,
        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast
    }

    public static class DirectionMapper
    {
        public static Direction Opposite(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.NorthWest:
                    return Direction.SouthEast;
                case Direction.NorthEast:
                    return Direction.SouthWest;
                case Direction.SouthWest:
                    return Direction.NorthEast;
                case Direction.SouthEast:
                    return Direction.NorthWest;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}