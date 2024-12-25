using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace SnakeSystem
{
    public class SnakeBodyPart : SnakePart
    {
        public override void Follow(SnakePart leader, Queue<TurnPoint> turnPoints)
        {
            if (leader == null) return;
            
            if (turnPoints.Count > 0 && new Vector2Int(X, Y) == turnPoints.Peek().Position)
            {
                var turnPoint = turnPoints.Peek();
                var direction = turnPoint.Direction;
                transform.rotation = Quaternion.Euler(Utilities.GetRotationByDirection(direction));
            }

            
            MoveTo(leader.previousX, leader.previousY);
            
            if (NextPart != null)
            {
                NextPart.Follow(this, turnPoints);
            }
            else
            {
                if (turnPoints.Count > 0 && new Vector2Int(previousX, previousY) == turnPoints.Peek().Position)
                {
                    turnPoints.Dequeue();
                }
            }
        }
    }
}
