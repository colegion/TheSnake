using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class DirectionButton : MonoBehaviour
    {
        [SerializeField] private Direction buttonDirection;
        [SerializeField] private Button button;
        
        private Coroutine _effectCoroutine;
        private Quaternion _initialRotation;
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnDirectionSelected()
        {
            EventBus.Instance.Trigger(new OnDirectionUpdated(buttonDirection));
        }

        private void ReverseSelf(OnDirectionMirrored e)
        {
            var duration = e.duration;
            _initialRotation = transform.localRotation;
            buttonDirection =  (Direction)(((int)buttonDirection + 2) % Enum.GetValues(typeof(Direction)).Length);
            transform.localRotation *= Quaternion.Euler(0, 0, 180f);
            StartCoroutine(WaitUntilEffectOver(duration));
        }

        private IEnumerator WaitUntilEffectOver(float duration)
        {
            yield return new WaitForSeconds(duration);
            ResetSelf();
        }

        private void ResetSelf()
        {
            transform.localRotation = _initialRotation;
            buttonDirection =  (Direction)(((int)buttonDirection + 2) % Enum.GetValues(typeof(Direction)).Length);
        }

        private void AddListeners()
        {
            button.onClick.AddListener(OnDirectionSelected);
            EventBus.Instance.Register<OnDirectionMirrored>(ReverseSelf);
        }

        private void RemoveListeners()
        {
            button.onClick.RemoveListener(OnDirectionSelected);
            EventBus.Instance.Unregister<OnDirectionMirrored>(ReverseSelf);
        }
    }
}
