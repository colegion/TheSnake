using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid
{
    private GridCell[,] _world;
    public int Width { get; private set; }
    public int Height { get; private set; }

    private List<GridCell> _availableCells;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        _world = new GridCell[width, height];
        _availableCells = new List<GridCell>();
    }

    public void AppendAvailableCells(GridCell cell)
    {
        if (_availableCells.Contains(cell)) return;
        _availableCells.Add(cell);
    }

    public void RemoveCellFromAvailableCells(GridCell cell)
    {
        if (_availableCells.Contains(cell))
        {
            _availableCells.Remove(cell);
        }
    }

    public GridCell GetAvailableRandomCell()
    {
        var index = Random.Range(0, _availableCells.Count);
        return _availableCells[index];
    }

    public bool HasCrashed(int x, int y)
    {
        return !_world[x, y].IsTileAvailableForLayer(Utilities.BlockLayer);
    }

    public bool IsCellHasFood(int x, int y, out Food food)
    {
        var headCell = _world[x, y];

        var tile = headCell.GetTile(Utilities.FoodLayer);
        if (tile != null && tile.TryGetComponent(out Food tileFood))
        {
            food = tileFood;
            return true;
        }
        else
        {
            food = null;
            return false;
        }
    }

    public void SetCell(GridCell cell)
    {
        if (_world[cell.X, cell.Y] != null)
        {
            Debug.LogError($"Specified coordinate already holds for another cell! Coordinate: {cell.X} {cell.Y}");
        }
        else
        {
            _world[cell.X, cell.Y] = cell;
        }
    }

    public void PlaceTileToParentCell(BaseTile tile)
    {
        var cell = _world[tile.X, tile.Y];
        if (cell == null)
        {
            Debug.LogWarning($"Given tile has no valid coordinate X: {tile.X} Y: {tile.Y}");
        }
        else
        {
            cell.SetTile(tile);
        }
    }

    public void ClearTileOfParentCell(BaseTile tile)
    {
        var cell = _world[tile.X, tile.Y];
        if (cell == null)
        {
            Debug.LogWarning($"Given tile has no valid coordinate X: {tile.X} Y: {tile.Y}");
        }
        else
        {
            cell.SetTileNull(tile.Layer);
        }
    }

    public Transform GetCellTargetByCoordinate(int x, int y)
    {
        return _world[x, y].GetTarget();
    }

    public int GetAvailableCellCount()
    {
        return _availableCells.Count;
    }
}