using Interfaces;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public abstract class Food : BaseTile, IConsumable
    {
        [SerializeField] private ParticleSystem eatEffect;
        public bool IsActive { get; set; }

        public virtual void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            //gameObject.SetActive(false);
            Grid.ClearTileOfParentCell(this);
        }

        public void PlayParticleEffect()
        {
            eatEffect.Play();
        }

        public abstract void OnConsume(Snake snake);
    }
}