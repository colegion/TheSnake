using Interfaces;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public abstract class Food : BaseTile, IConsumable
    {
        [SerializeField] private Color effectColor;
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


        public Color GetColor()
        {
            return effectColor;
        }
        
        public abstract void OnConsume(Snake snake);
    }
}