using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace SnakeSystem
{
    public class SnakePart : BaseTile
    {
        public SnakeBodyPart NextPart { get; private set; }
        public SnakeBodyPart PreviousPart { get; private set; }
        
        [FormerlySerializedAs("_previousX")] public int previousX;
        [FormerlySerializedAs("_previousY")] public int previousY;

        public void SetNextPart(SnakeBodyPart nextPart)
        {
            NextPart = nextPart;
        }

        public void SetPreviousPart(SnakeBodyPart previousPart)
        {
            PreviousPart = previousPart;
        }

        public void MoveTo(int newX, int newY)
        {
            previousX = X;
            previousY = Y;
            
            Grid.ClearTileOfParentCell(this);
            SetXCoordinate(newX);
            SetYCoordinate(newY);
            Grid.PlaceTileToParentCell(this);
            var target = new Vector3(newX, transform.position.y, newY);
            transform.DOMove(target, Utilities.Tick / 4f).SetEase(Ease.Linear);
        }

        public virtual void Follow(SnakePart leader, Queue<TurnPoint> turnPoints)
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
                if (turnPoints.Count > 0 && new Vector2Int(X, Y) == turnPoints.Peek().Position)
                {
                    turnPoints.Dequeue(); // Remove the turn point after the last part processes it
                }
            }
        }
    }
}