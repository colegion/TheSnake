using System.Collections;
using System.Collections.Generic;
using Helpers;
using Mono.Cecil;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform gridParent;

    private const string LightTilePath = "Prefabs/GrassLight";
    private const string DarkTilePath = "Prefabs/GrassDark";
    
    public void GenerateGrid(Size size)
    {
        var width = size.width;
        var height = size.height;

        var lightPrefab = Resources.Load<GameObject>(LightTilePath);
        var darkPrefab = Resources.Load<GameObject>(DarkTilePath);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var isWhite = (x + y) % 2 == 0;
                var tilePrefabPath = isWhite ? lightPrefab : darkPrefab;

                var tile = Instantiate(tilePrefabPath, new Vector3(x, 0, y), Quaternion.identity, gridParent.transform);
                tile.name = $"{tile.name}{x}_{y}";
            }
        }
    }
}
