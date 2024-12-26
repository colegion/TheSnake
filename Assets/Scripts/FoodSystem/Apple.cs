using Helpers;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public class Apple : Food
    {
        public override void OnConsume(Snake snake)
        {
            snake.Grow();
            Grid.ClearTileOfParentCell(this);
            Deactivate();
            EventBus.Instance.Trigger(new OnAppleGathered());
        }
    }
}
