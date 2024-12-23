using Interfaces;
using SnakeSystem;

namespace FoodSystem
{
    public abstract class Food : BaseTile, IConsumable
    {
        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public abstract void OnConsume(Snake snake);
    }
}