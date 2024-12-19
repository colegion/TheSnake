using System.Collections;
using System.Collections.Generic;
using Helpers;
using Mono.Cecil;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform gridParent;

    private Grid _grid;

    private const string LightTilePath = "Prefabs/GrassLight";
    private const string DarkTilePath = "Prefabs/GrassDark";
    
    public void GenerateGrid(Size size)
    {
        _grid = new Grid();
        _grid.InitiateWorld(size);
        var width = size.width;
        var height = size.height;

        if (width == 0 || height == 0)
        {
            Debug.LogWarning("Please enter values bigger than 0 for width and height");
            return;
        }

        var lightPrefab = Resources.Load<GridCell>(LightTilePath);
        var darkPrefab = Resources.Load<GridCell>(DarkTilePath);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var isWhite = (x + y) % 2 == 0;
                var cellPrefabPath = isWhite ? lightPrefab : darkPrefab;
                
                var cell = Instantiate(cellPrefabPath, new Vector3(x, 0, y), Quaternion.identity, gridParent.transform);
                cell.ConfigureSelf(x, y);
                _grid.SetCell(cell);
            }
        }
    }
}
