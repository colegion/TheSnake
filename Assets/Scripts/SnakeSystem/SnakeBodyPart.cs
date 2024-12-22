namespace SnakeSystem
{
    public class SnakeBodyPart : SnakePart
    {
        public override void Follow(SnakePart leader)
        {
            if (leader == null) return;
            
            SetXCoordinate(leader.previousX);
            SetYCoordinate(leader.previousY);
            MoveTo(leader.previousX, leader.previousY);
            
            if (NextPart != null)
            {
                NextPart.Follow(this);
            }
        }
    }
}
