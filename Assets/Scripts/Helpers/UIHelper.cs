using System;
using TMPro;
using UnityEngine;

namespace Helpers
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelIndexField;
        [SerializeField] private TextMeshProUGUI targetField;

        private int _levelTarget;
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void ConfigureInitialValues(OnLevelStartEvent e)
        {
            levelIndexField.text = $"Level: {e.levelIndex}";
            _levelTarget = e.target;
            targetField.text = $"0 / {_levelTarget}";
        }

        private void AddListeners()
        {
            EventBus.Instance.Register<OnLevelStartEvent>(ConfigureInitialValues);
        }

        private void RemoveListeners()
        {
            EventBus.Instance.Unregister<OnLevelStartEvent>(ConfigureInitialValues);
        }
    }
}
