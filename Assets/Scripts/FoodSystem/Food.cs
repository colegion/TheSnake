using Interfaces;
using SnakeSystem;

namespace FoodSystem
{
    public abstract class Food : BaseTile, IConsumable
    {
        public bool IsActive { get; set; }

        public virtual void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
            Grid.ClearTileOfParentCell(this);
        }

        public abstract void OnConsume(Snake snake);
    }
}