using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace SnakeSystem
{
    public class SnakeBodyPart : SnakePart
    {
        public override void Follow(SnakePart leader, Queue<TurnPoint> turnPoints, Direction direction, int bodyIndex)
        {
            if (leader == null) return;
            
            if (turnPoints.Count > 0 && new Vector2Int(X, Y) == turnPoints.Peek().Position)
            {
                var turnPoint = turnPoints.Peek();
                var trurnDirection = turnPoint.Direction;
                transform.rotation = Quaternion.Euler(Utilities.GetRotationByDirection(trurnDirection));
            }
            
            MoveTo(leader.previousX, leader.previousY, direction, bodyIndex);
            if (NextPart != null)
            {
                NextPart.Follow(this, turnPoints, direction, bodyIndex+1);
            }
            else if (turnPoints.Count > 0 && new Vector2Int(previousX, previousY) == turnPoints.Peek().Position)
            {
                turnPoints.Dequeue();
            }
        }

    }
}
