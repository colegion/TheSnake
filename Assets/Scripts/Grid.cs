using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Grid
{
   private GridCell[,] _world;

   public void InitiateWorld(Size size)
   {
      _world = new GridCell[size.width, size.height];
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
}
