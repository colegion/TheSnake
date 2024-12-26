using System.Collections;
using Helpers;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public class Wallify : Food
    {
        [SerializeField] private GameObject visuals;
        [SerializeField] private float activeDuration;
        [SerializeField] private float effectDuration;
        
        public override void Activate()
        {
            base.Activate();
            visuals.gameObject.SetActive(true);
            StartCoroutine(ActivateSelfForDuration());
        }
        
        public override void OnConsume(Snake snake)
        {
            StopCoroutine(ActivateSelfForDuration());
            EventBus.Trigger(new OnWallifyConsumed(effectDuration));
            Deactivate();
            StartCoroutine(DeactivateAfterConsumed());
        }

        private IEnumerator ActivateSelfForDuration()
        {
            yield return new WaitForSeconds(activeDuration);
            IsActive = false;
            Deactivate();
        }
        
        private IEnumerator DeactivateAfterConsumed()
        {
            yield return new WaitForSeconds(activeDuration);
            IsActive = false;
        }
        
        public override void Deactivate()
        {
            visuals.gameObject.SetActive(false);
            Grid.ClearTileOfParentCell(this);
        }
        
    }
}
