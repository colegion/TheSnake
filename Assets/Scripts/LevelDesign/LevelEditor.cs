using System.Collections.Generic;
using System.Drawing;
using Helpers;
using SnakeSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Size = Helpers.Size;

namespace LevelDesign
{
    public class LevelEditor : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        private LevelSaver _levelSaver;

        [HideInInspector] public int width;
        [HideInInspector] public int height;
        [HideInInspector] public int targetCount = 5;
        [HideInInspector] public bool gridGenerated;
        [HideInInspector] public List<Wall> walls;
        [HideInInspector] public Snake snake;
        
        public void CreateGrid()
        {
            var size = new Size(width, height);
            levelGenerator.GenerateGrid(size);
            gridGenerated = true;
        }

        public void AddWall()
        {
            if (walls == null) walls = new List<Wall>();
            
            var wall = levelGenerator.SpawnTileByPath(Utilities.WallPath);
            wall.ConfigureSelf(0, 0);
            
            ((Wall)wall).SetWallType(WallType.Concrete);
            walls.Add((Wall)wall);
        }

        public void AddSnake()
        {
            if (snake != null) return;

            snake = (Snake)levelGenerator.SpawnTileByPath(Utilities.SnakePath);
            snake.ConfigureSelf(0, 0);
        }

        public void RemoveWall(Wall wall)
        {
            walls.Remove(wall);
            Destroy(wall.gameObject);
        }

        public void SaveLevel()
        {
            _levelSaver = new LevelSaver();
            var nextIndex = _levelSaver.GetMaxExistingLevelIndex();
            List<WallData> wallData = new List<WallData>();
            LevelData data = new LevelData();

            foreach (var wall in walls)
            {
                wallData.Add((WallData)wall.CreateTileData());
            }
            
            data.width = width;
            data.height = height;
            data.wallData = wallData;
            data.snakeData = (SnakeData)snake.CreateTileData();
            data.target = targetCount;
            
            _levelSaver.SaveLevelWithIndex(data, nextIndex+1);
        }
        
    }
}
