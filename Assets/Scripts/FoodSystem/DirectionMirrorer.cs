using System.Collections;
using Helpers;
using SnakeSystem;
using UnityEngine;

namespace FoodSystem
{
    public class DirectionMirrorer : Food
    {
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
        }
    }
}
