using System.Collections.Generic;
using Helpers;
using UnityEngine.Serialization;

namespace SnakeSystem
{
    public class SnakePart : BaseTile
    {
        public SnakePart NextPart { get; private set; }
        public SnakePart PreviousPart { get; private set; }
        
        [FormerlySerializedAs("_previousX")] public int previousX;
        [FormerlySerializedAs("_previousY")] public int previousY;

        public void SetNextPart(SnakePart nextPart)
        {
            NextPart = nextPart;
        }

        public void SetPreviousPart(SnakePart previousPart)
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
            SetLocalPosition(-newX, -newY);
        }

        public virtual void Follow(SnakePart leader, Queue<TurnPoint> turnPoints)
        {
            if (leader == null) return;
            
            MoveTo(-leader.previousX, -leader.previousY);
            
            if (NextPart != null)
            {
                NextPart.Follow(this, turnPoints);
            }
        }
    }
}