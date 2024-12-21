using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using SnakeSystem;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CameraAdjuster cameraAdjuster; 
    [SerializeField] private LevelGenerator levelGenerator;

    private LevelSaver _levelSaver;
    private Grid _grid;
    private int _target = 0;
    private Snake _snake;
    
    private Coroutine _movementRoutine;
    private bool _isGameFinished;
    private void Start()
    {
        _levelSaver = new LevelSaver();
        LoadLevel();
    }

    private void LoadLevel()
    {
        var index = TryGetLevelIndex();
        TextAsset levelFile = Resources.Load<TextAsset>($"Levels/level{index}");
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
                EventBus.Instance.Trigger(new OnLevelStartEvent(index, _target));
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
            yield return new WaitForSeconds(1f);
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
}
