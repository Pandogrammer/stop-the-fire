using System;

namespace Utils.Unity.ProceduralLevel.Scripts
{
    public struct Position
    {
        public readonly int x;
        public readonly int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Position operator +(Position a, Position b) => new Position(a.x + b.x, a.y + b.y);
        public static bool operator ==(Position a, Position b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Position a, Position b) => !(a == b);

        public static float Distance(Position a, Position b)
        {
            var x = b.x - a.x;
            var y = b.y - a.y;
            var sum = Math.Pow(x, 2) + Math.Pow(y, 2);
            return (float) Math.Sqrt(sum);
        }
    }

    public static class PositionExtensions
    {
        public static UnityEngine.Vector3 ToIsometricVector3(this Position position)
        {
            return new UnityEngine.Vector3(position.x, 0, position.y);
        }
        public static Position Go(this Position position, Direction direction)
        {
            var x = position.x;
            var y = position.y;
            switch(direction)
            {
                case Direction.North:
                    return new Position(x, y+1);
                case Direction.West:
                    return new Position(x-1, y);
                case Direction.East:
                    return new Position(x+1, y);
                case Direction.South:
                    return new Position(x, y-1);
                case Direction.NorthWest:
                    return new Position(x-1, y+1);
                case Direction.NorthEast:
                    return new Position(x+1, y+1);
                case Direction.SouthWest:
                    return new Position(x-1, y-1);
                case Direction.SouthEast:
                    return new Position(x+1, y-1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}