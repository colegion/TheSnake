namespace SnakeSystem
{
    public class SnakeBodyPart : SnakePart
    {
        public override void Follow(SnakePart leader)
        {
            if (leader == null) return;

            // Update position to match the leader's previous position
            SetXCoordinate(leader.X);
            SetYCoordinate(leader.Y);

            // Pass the follow command to the next part
            if (NextPart != null)
            {
                NextPart.Follow(this);
            }
        }
    }
}
