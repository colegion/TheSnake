using System.Collections.Generic;
using System.Drawing;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Size = Helpers.Size;

namespace LevelDesign
{
    public class LevelEditor : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;

        [HideInInspector] public int width;
        [HideInInspector] public int height;
        [HideInInspector] public bool gridGenerated;
        [HideInInspector] public WallType selectedType;

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
            ((Wall)wall).SetWallType(WallType.Concrete);
            walls.Add((Wall)wall);
        }

        public void AddSnake()
        {
            if (snake != null) return;

            snake = (Snake)levelGenerator.SpawnTileByPath(Utilities.SnakePath);
        }

        public void RemoveWall(Wall wall)
        {
            walls.Remove(wall);
            Destroy(wall.gameObject);
        }
        
    }
}
