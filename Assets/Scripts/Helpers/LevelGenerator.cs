using System.Collections.Generic;
using SnakeSystem;
using Unity.Mathematics;
using UnityEngine;

namespace Helpers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform gridParent;

        private Grid _grid;
        private Snake _snake;

        public void GenerateLevelFromJson(Grid grid, LevelData data)
        {
            var size = new Size(data.width, data.height);
            _grid = grid;
            GenerateGridVisual(size);

            foreach (var wallData in data.wallData)
            {
                var wall = SpawnTileByPath(Utilities.WallPath);
                ConfigureTile(wall, wallData);
                wall.SetLayer((int)wallData.type);
                wall.InjectController(grid);
                ((Wall)wall).SetWallType(wallData.type);
            }

            _snake = (Snake)SpawnTileByPath(Utilities.SnakePath);
            _snake.InjectController(grid);
            _snake.SetDirection(data.snakeData.initialDirection);
            ConfigureTile(_snake, data.snakeData);
        }
    
        public void GenerateGrid(Size size)
        {
            if (size.width <= 0 || size.height <= 0)
            {
                Debug.LogWarning("Please enter values bigger than 0 for width and height");
                return;
            }
            
            _grid = new Grid(size.width, size.height);
            
            GenerateGridVisual(size);
        }

        private void GenerateGridVisual(Size size)
        {
            var lightPrefab = Resources.Load<GridCell>(Utilities.LightTilePath);
            var darkPrefab = Resources.Load<GridCell>(Utilities.DarkTilePath);

            for (int x = 0; x < size.width; x++)
            {
                for (int y = 0; y < size.height; y++)
                {
                    var isWhite = (x + y) % 2 == 0;
                    var cellPrefab = isWhite ? lightPrefab : darkPrefab;

                    var cell = Instantiate(cellPrefab, new Vector3(x, 0, y), Quaternion.identity, gridParent.transform);
                    cell.ConfigureSelf(x, y);
                    _grid.SetCell(cell);
                }
            }
        }


        public BaseTile SpawnTileByPath(string path)
        {
            var prefab = Resources.Load<BaseTile>(path);
            var tile = Instantiate(prefab, Vector3.zero, quaternion.identity, gridParent.transform);
            return tile;
        }

        private void ConfigureTile(BaseTile tile, SaveData data = null)
        {
            var x = data?.x ?? 0;
            var y = data?.y ?? 0;
            tile.ConfigureSelf(x, y);
        }

        public Snake GetSnake()
        {
            return _snake;
        }
    }
}
