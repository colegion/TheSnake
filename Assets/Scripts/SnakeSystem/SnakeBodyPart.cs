namespace SnakeSystem
{
    public class SnakeBodyPart : SnakePart
    {
        public override void Follow(SnakePart leader)
        {
            if (leader == null) return;
            
            SetXCoordinate(leader.X);
            SetYCoordinate(leader.Y);
            
            if (NextPart != null)
            {
                NextPart.Follow(this);
            }
        }
    }
}
