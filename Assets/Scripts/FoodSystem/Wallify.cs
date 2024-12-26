using System.Collections;
using Helpers;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public class Wallify : Food
    {
        [SerializeField] private float activeDuration;
        [SerializeField] private float effectDuration;
        
        public override void Activate()
        {
            base.Activate();
            StartCoroutine(ActivateSelfForDuration());
        }
        
        public override void OnConsume(Snake snake)
        {
            EventBus.Trigger(new OnWallifyConsumed(effectDuration));
            Deactivate();
        }

        private IEnumerator ActivateSelfForDuration()
        {
            yield return new WaitForSeconds(activeDuration);
            Deactivate();
        }
        
    }
}
