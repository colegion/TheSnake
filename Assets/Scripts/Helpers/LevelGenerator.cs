using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Helpers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform gridParent;

        private Grid _grid;
        private List<Wall> _walls;

        public void GenerateLevelFromJSON(LevelData data)
        {
            var size = new Size(data.width, data.height);
            GenerateGrid(size);

            foreach (var wallData in data.wallData)
            {
                var wall = SpawnTileByPath(Utilities.WallPath, wallData);
                ((Wall)wall).SetWallType(wallData.type);
            }

            var snake = SpawnTileByPath(Utilities.SnakePath, data.snakeData);
            ((Snake)snake).SetDirection(data.snakeData.initialDirection);
        }
    
        public void GenerateGrid(Size size)
        {
            _walls = new List<Wall>();
            _grid = new Grid();
            _grid.InitiateWorld(size);
            var width = size.width;
            var height = size.height;

            if (width == 0 || height == 0)
            {
                Debug.LogWarning("Please enter values bigger than 0 for width and height");
                return;
            }

            var lightPrefab = Resources.Load<GridCell>(Utilities.LightTilePath);
            var darkPrefab = Resources.Load<GridCell>(Utilities.DarkTilePath);
        
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

        public BaseTile SpawnTileByPath(string path, SaveData data = null)
        {
            var prefab = Resources.Load<BaseTile>(path);
            var tile = Instantiate(prefab, Vector3.zero, quaternion.identity, gridParent.transform);
            var x = data?.x ?? 0;
            var y = data?.y ?? 0;
            tile.ConfigureSelf(x, y);
            return tile;
        }
    }
}
