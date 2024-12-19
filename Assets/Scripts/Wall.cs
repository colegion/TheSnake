using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Wall : BaseTile
{
    [SerializeField] private MeshFilter wallMeshFilter;
    private WallType _wallType;

    public void SetWallType(WallType type)
    {
        _wallType = type;
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        wallMeshFilter.mesh = Utilities.GetWallMeshByType(_wallType);
    }

    public WallType GetWallType()
    {
        return _wallType;
    }
    
    public override SaveData CreateTileData()
    {
        var data = new WallData
        {
            x = _x,
            y = _y,
            type = _wallType
        };
        return data;
    }
}
