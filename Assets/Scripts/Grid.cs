using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Grid
{
   private GridCell[,] _world;
   public int Width { get; private set; }
   public int Height { get; private set; }

   public Grid(int width, int height)
   {
      Width = width;
      Height = height;
      _world = new GridCell[width, height];
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
}
