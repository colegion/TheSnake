using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;

    private LevelSaver _levelSaver;
    private Grid _grid;

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
                _grid = new Grid(levelData.width, levelData.height);
                levelGenerator.GenerateLevelFromJson(_grid, levelData);
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
