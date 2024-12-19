using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Snake : BaseTile
{
    [SerializeField] private GameObject visuals;

    private Direction _direction = Direction.Up;

    public void RotateSelf()
    {
        _direction = (Direction)(((int)_direction + 1) % Enum.GetValues(typeof(Direction)).Length);
        visuals.transform.localRotation = Quaternion.Euler(Utilities.GetRotationByDirection(_direction));
    }
    
    public override SaveData CreateTileData()
    {
        var data = new SnakeData
        {
            x = _x,
            y = _y,
            initialDirection = _direction
        };
        return data;
    }
}
