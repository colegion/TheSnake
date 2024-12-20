using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEditor.Playables;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int _x;
    private int _y;

    public int X => _x;
    public int Y => _y;

    private Dictionary<int, BaseTile> _tiles = new Dictionary<int, BaseTile>();

    public void ConfigureSelf(int x, int y)
    {
        _x = x;
        _y = y;
        name = $"{name}{x}_{y}";
        SetRotation();
    }

    private void SetRotation()
    {
        if (Utilities.IsEdgeCell(_y))
        {
            transform.localRotation = Quaternion.Euler(Vector3.down * 90f);
        }
    }
    
    public void SetTile(BaseTile baseTile)
    {
        _tiles[baseTile.Layer] = baseTile;
    }
    
    public void SetTileNull(int layer)
    {
        if (!_tiles.ContainsKey(layer)) return;
            
        var tile = _tiles.GetValueOrDefault(layer);
        if (tile != null)
        {
            _tiles[layer] = null;
        }
    }

    public bool IsTileAvailable(int layer)
    {
        if (!_tiles.ContainsKey(layer)) return true;
        return _tiles[layer] == null;
    }

    public BaseTile GetTile(int layer)
    {
        return _tiles.GetValueOrDefault(layer);
    }
}
