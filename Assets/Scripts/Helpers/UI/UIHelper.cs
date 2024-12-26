using System;
using Helpers.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helpers
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelIndexField;
        [SerializeField] private TextMeshProUGUI targetField;
        [SerializeField] private Canvas mainCanvas;
        [FormerlySerializedAs("infoPopup")] [SerializeField] private UIElementLoader uıElementLoader;

        private int _levelTarget;
        private int _currentCount = 0;
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void ConfigureInitialValues(OnLevelStart e)
        {
            levelIndexField.text = $"Level: {e.levelIndex}";
            _levelTarget = e.target;
            _currentCount = 0;
            targetField.text = $"{_currentCount} / {_levelTarget}";
        }

        private void HandleOnAppleGathered(OnAppleGathered e)
        {
            _currentCount++;
            targetField.text = $"{_currentCount} / {_levelTarget}";
        }

        private void HandleOnGameOver(OnGameOver e)
        {
            uıElementLoader.LoadPopup(e.isSuccess, mainCanvas.transform);
        }

        private void AddListeners()
        {
            EventBus.Register<OnLevelStart>(ConfigureInitialValues);
            EventBus.Register<OnAppleGathered>(HandleOnAppleGathered);
            EventBus.Register<OnGameOver>(HandleOnGameOver);
        }

        private void RemoveListeners()
        {
            EventBus.Unregister<OnLevelStart>(ConfigureInitialValues);
            EventBus.Unregister<OnAppleGathered>(HandleOnAppleGathered);
            EventBus.Unregister<OnGameOver>(HandleOnGameOver);
        }
    }
}
