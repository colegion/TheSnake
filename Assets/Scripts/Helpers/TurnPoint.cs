using UnityEngine;

namespace Helpers
{
    public class TurnPoint
    {
        public Vector2Int Position { get; }
        public Direction Direction { get; }

        public TurnPoint(Vector2Int position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
    }

}
