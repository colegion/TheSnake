using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Snake : BaseTile
{
    [SerializeField] private GameObject visuals;

    private Direction _direction = Direction.Up;
    
    public void SetDirection(Direction direction)
    {
        _direction = direction;
        SetRotation();
    }
    
    public void UpdateDirection()
    {
        _direction = (Direction)(((int)_direction + 1) % Enum.GetValues(typeof(Direction)).Length);
        SetRotation();
    }

    private void SetRotation()
    {
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
