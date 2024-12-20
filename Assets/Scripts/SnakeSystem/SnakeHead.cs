using UnityEngine;

namespace SnakeSystem
{
    public class SnakeHead : SnakePart
    {
        public void Move(Vector2Int direction)
        {
            int newX = X + direction.x;
            int newY = Y + direction.y;
            
            Grid.ClearTileOfParentCell(this);
            SetXCoordinate(newX);
            SetYCoordinate(newY);
            Grid.PlaceTileToParentCell(this);
            
            if (NextPart != null)
            {
                NextPart.Follow(this);
            }
        }

        public void Grow()
        {
            /*// Create a new body part
            var newBodyPart = Instantiate(SnakePartPrefab); // Reference to the prefab
            newBodyPart.ConfigureSelf(X, Y); // Position it at the head's current position

            // Link the new body part between the head and the first body segment
            newBodyPart.SetNextPart(NextPart);
            if (NextPart != null)
            {
                NextPart.SetPreviousPart(newBodyPart);
            }
            newBodyPart.SetPreviousPart(this);
            SetNextPart(newBodyPart);*/
        }
    }
}