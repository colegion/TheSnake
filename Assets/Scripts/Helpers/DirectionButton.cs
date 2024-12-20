using System;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class DirectionButton : MonoBehaviour
    {
        [SerializeField] private Direction buttonDirection;
        [SerializeField] private Button button;
        
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

        private void AddListeners()
        {
            button.onClick.AddListener(OnDirectionSelected);
        }

        private void RemoveListeners()
        {
            button.onClick.RemoveListener(OnDirectionSelected);
        }
    }
}
