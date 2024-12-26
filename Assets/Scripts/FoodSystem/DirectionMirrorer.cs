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
            StartCoroutine(DisableAfterInterval(activeDuration));
        }

        private IEnumerator DisableAfterInterval(float interval)
        {
            yield return new WaitForSeconds(interval);
            Deactivate();
        }

        public override void OnConsume(Snake snake)
        {
            EventBus.Instance.Trigger(new OnDirectionMirrored(effectDuration));
            Deactivate();
            StartCoroutine(DeactivateAfterConsumed());
        }

        private IEnumerator DeactivateAfterConsumed()
        {
            yield return new WaitForSeconds(effectDuration);
            IsActive = false;
            gameObject.SetActive(false);
        }
        
        public override void Deactivate()
        {
            visuals.gameObject.SetActive(false);
            Grid.ClearTileOfParentCell(this);
        }
    }
}
