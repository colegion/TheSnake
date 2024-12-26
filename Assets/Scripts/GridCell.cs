using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEditor.Playables;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Transform target;
    private int _x;
    private int _y;

    public int X => _x;
    public int Y => _y;

    private Dictionary<int, BaseTile> _tiles = new Dictionary<int, BaseTile>();
    private Grid _grid;

    public void InjectGrid(Grid grid)
    {
        _grid = grid;
    }

    public void ConfigureSelf(int x, int y)
    {
        _x = x;
        _y = y;
        name = $"{name}{x}_{y}";
        SetRotation();
        _grid.AppendAvailableCells(this);
    }

    private void SetRotation()
    {
        if (Utilities.IsEdgeCell(_y))
        {
            transform.localRotation = Quaternion.Euler(Vector3.down * (90f + (transform.parent.eulerAngles.y - 360f)));
        }
    }
    
    public void SetTile(BaseTile baseTile)
    {
        _tiles[baseTile.Layer] = baseTile;
        _grid.RemoveCellFromAvailableCells(this);
    }
    
    public void SetTileNull(int layer)
    {
        if (!_tiles.ContainsKey(layer)) return;
            
        var tile = _tiles.GetValueOrDefault(layer);
        if (tile != null)
        {
            _tiles[layer] = null;
        }
        
        if(IsTilesAllFree())
            _grid.AppendAvailableCells(this);
    }

    public bool IsTileAvailableForLayer(int layer)
    {
        if (!_tiles.ContainsKey(layer)) return true;
        return _tiles[layer] == null;
    }

    public bool IsTilesAllFree()
    {
        foreach (var tile in _tiles.Values)
        {
            if (tile != null)
            {
                return false;
            }
        }
        return true;
    }
    
    public BaseTile GetTile(int layer)
    {
        return _tiles.GetValueOrDefault(layer);
    }

    public Transform GetTarget()
    {
        return target;
    }
}
