using System;
using System.Collections.Generic;
using SnakeSystem;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Helpers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform gridParent;

        private Grid _grid;
        private Snake _snake;
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        public void GenerateLevelFromJson(Grid grid, LevelData data)
        {
            var size = new Size(data.width, data.height);
            _grid = grid;
            GenerateGridVisual(size);

            foreach (var wallData in data.wallData)
            {
                ConfigureNewWall(wallData);
            }

            _snake = (Snake)SpawnTileByPath(Utilities.SnakePath);
            _snake.SetLayer(Utilities.BlockLayer);
            _snake.InjectGrid(grid);
            _snake.SetDirection(data.snakeData.initialDirection);
            ConfigureTile(_snake, data.snakeData);
        }

        private Wall ConfigureNewWall(WallData wallData)
        {
            var wall = WallPool.Instance.GetAvailableWall();
            ConfigureTile(wall, wallData);
            wall.SetLayer((int)wallData.type);
            wall.InjectGrid(_grid);
            wall.SetWallType(wallData.type);
            return wall;
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
                    cell.InjectGrid(_grid);
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

        private void WallifyGrid(OnWallifyConsumed e)
        {
                var min = Mathf.Min(3, _grid.GetAvailableCellCount() / 10);
                var max = Mathf.Max(3, _grid.GetAvailableCellCount() / 10);
                var randomCount = Random.Range(min, max);
                for (int i = 0; i < randomCount; i++)
                {
                    var randomCell = _grid.GetAvailableRandomCell();
                    var wallData = new WallData()
                    {
                        type = WallType.Concrete,
                        x = randomCell.X,
                        y = randomCell.Y
                    };
                
                    var wall = ConfigureNewWall(wallData);
                    wall.DisableSelfAfterInterval(e.duration);
                }
        }

        private void AddListeners()
        {
            EventBus.Instance.Register<OnWallifyConsumed>(WallifyGrid);
        }

        private void RemoveListeners()
        {
            EventBus.Instance.Unregister<OnWallifyConsumed>(WallifyGrid);
        }
    }
}
