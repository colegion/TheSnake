using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using Helpers;
using Helpers.UI;
using SnakeSystem;
using UnityEngine;
using UnityEngine.XR;
using AudioType = Helpers.AudioType;

public class GameController : MonoBehaviour
{
    [SerializeField] private CameraAdjuster cameraAdjuster;
    [SerializeField] private FoodController foodController;
    [SerializeField] private LevelGenerator levelGenerator;

    private LevelSaver _levelSaver;
    private Grid _grid;
    private int _target = 0;
    private int _currentGatheredAppleCount = 0;
    private Snake _snake;
    
    private Coroutine _movementRoutine;
    private bool _isGameFinished;

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void Start()
    {
        _levelSaver = new LevelSaver();
        LoadLevel();
    }

    private void LoadLevel()
    {
        var index = TryGetLevelIndex();
        TextAsset levelFile = Resources.Load<TextAsset>($"Levels/level1");
        if (levelFile != null)
        {
            LevelData levelData = JsonUtility.FromJson<LevelData>(levelFile.text);

            if (levelData != null)
            {
                _target = levelData.target;
                _grid = new Grid(levelData.width, levelData.height);
                cameraAdjuster.AdjustCameraToGrid(levelData.width, levelData.height);
                levelGenerator.GenerateLevelFromJson(_grid, levelData);
                _snake = levelGenerator.GetSnake();
                EventBus.Instance.Trigger(new OnLevelStart(index, _target));
                InitiateTheGame();
            }
            else
            {
                Debug.LogError("Failed to parse level data.");
            }
        }
        else
        {
            Debug.LogError("Level file not found.");
        }
    }

    private void InitiateTheGame()
    {
        _movementRoutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        _snake.Initialize();
        while (!_isGameFinished)
        {
            _snake.Move();
            foodController.PlaceFoods(_grid);
            yield return new WaitForSeconds(Utilities.Tick);
        }
    }

    private int TryGetLevelIndex()
    {
        var index = PlayerPrefs.GetInt(Utilities.LevelIndexKey, 1);
        var maxIndex = _levelSaver.GetMaxExistingLevelIndex();

        if (index > maxIndex)
        {
            index = 1;
            PlayerPrefs.SetInt(Utilities.LevelIndexKey, index);
        }

        return index;
    }

    private void IncrementLevelIndex()
    {
        var currentIndex = PlayerPrefs.GetInt(Utilities.LevelIndexKey, 1);
        var maxIndex = _levelSaver.GetMaxExistingLevelIndex();
        currentIndex++;
        if (currentIndex > maxIndex)
        {
            currentIndex = 1;
        }
        
        PlayerPrefs.SetInt(Utilities.LevelIndexKey, currentIndex);
    }

    private void HandleOnAppleGathered(OnAppleGathered e)
    {
        _currentGatheredAppleCount++;
        if (_currentGatheredAppleCount == _target)
        {
            IncrementLevelIndex();
            EventBus.Instance.Trigger(new OnGameOver(true));
        }
    }

    private void HandleOnGameOver(OnGameOver e)
    {
        StopGame();
    }

    private void StopGame()
    {
        _isGameFinished = true;
        if(_movementRoutine != null)
            StopCoroutine(_movementRoutine);
    }
    
    private void AddListeners()
    {
        EventBus.Instance.Register<OnAppleGathered>(HandleOnAppleGathered);
        EventBus.Instance.Register<OnGameOver>(HandleOnGameOver);
    }

    private void RemoveListeners()
    {
        EventBus.Instance.Unregister<OnAppleGathered>(HandleOnAppleGathered);
        EventBus.Instance.Unregister<OnGameOver>(HandleOnGameOver);
    }
}
