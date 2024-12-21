namespace SnakeSystem
{
    public class SnakePart : BaseTile
    {
        public SnakePart NextPart { get; private set; }
        public SnakePart PreviousPart { get; private set; }
        
        private int _previousX;
        private int _previousY;

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
            _previousX = X;
            _previousY = Y;
            
            Grid.ClearTileOfParentCell(this);
            SetXCoordinate(newX);
            SetYCoordinate(newY);
            Grid.PlaceTileToParentCell(this);
        }

        public virtual void Follow(SnakePart leader)
        {
            if (leader == null) return;
            
            MoveTo(leader._previousX, leader._previousY);
            
            if (NextPart != null)
            {
                NextPart.Follow(this);
            }
        }
    }
}