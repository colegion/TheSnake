using System.Collections;
using Helpers;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public class DirectionMirrorer : Food
    {
        [SerializeField] private GameObject visuals;
        [SerializeField] private float activeDuration;
        [SerializeField] private float effectDuration;
        public override void Activate()
        {
            base.Activate();
            visuals.gameObject.SetActive(true);
            StartCoroutine(DisableAfterInterval(activeDuration));
        }

        private IEnumerator DisableAfterInterval(float interval)
        {
            yield return new WaitForSeconds(interval);
            IsActive = false;
            Deactivate();
        }

        public override void OnConsume(Snake snake)
        {
            EventBus.Trigger(new OnDirectionMirrored(effectDuration));
            Deactivate();
            StopCoroutine(DisableAfterInterval(activeDuration));
            StartCoroutine(DeactivateAfterConsumed());
        }

        private IEnumerator DeactivateAfterConsumed()
        {
            yield return new WaitForSeconds(effectDuration);
            IsActive = false;
        }
        
        public override void Deactivate()
        {
            visuals.gameObject.SetActive(false);
            Grid.ClearTileOfParentCell(this);
        }
    }
}
