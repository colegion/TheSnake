using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        [SerializeField] private List<WallConfig> wallConfigs;
        private static List<WallConfig> _wallConfigs;
        
        public const string LightTilePath = "Prefabs/GrassLight";
        public const string DarkTilePath = "Prefabs/GrassDark";
        public const string WallPath = "Prefabs/Wall";
        public const string SnakePath = "Prefabs/Snake";

        public const string LevelIndexKey = "LevelIndex";

        public const int BlockLayer = 0;
        public const int FreeLayer = 1;
        public const int FoodLayer = 2;

        private static Dictionary<Direction, Vector2Int> _directionVectors = new Dictionary<Direction, Vector2Int>()
        {
            { Direction.Up , Vector2Int.up },
            { Direction.Right , Vector2Int.right },
            { Direction.Down , Vector2Int.down },
            { Direction.Left , Vector2Int.left },
        };

        private void Start()
        {
            _wallConfigs = wallConfigs;
        }

        public static Vector3 GetRotationByDirection(Direction direction)
        {
            return Vector3.up * 90 * (int)direction;
        }

        public static Mesh GetWallMeshByType(WallType type)
        {
            return _wallConfigs.Find(c => c.type == type).wallMesh;
        }

        public static Vector2Int GetDirectionVector(Direction direction)
        {
            return _directionVectors[direction];
        }
        
        
        public static bool IsEdgeCell(int yCoord) => yCoord == 0;
    }

    [Serializable]
    public class LevelData
    {
        public int width;
        public int height;
        public List<WallData> wallData;
        public SnakeData snakeData;
        public int target;
    }

    [Serializable]
    public class WallData : SaveData
    {
        public WallType type;
    }

    [Serializable]
    public class SnakeData : SaveData
    {
        public Direction initialDirection;
    }

    [Serializable]
    public class SaveData
    {
        public int x;
        public int y;
    }
    
    [Serializable]
    public class Size
    {
        public int width;
        public int height;

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }

    [Serializable]
    public class WallConfig
    {
        public WallType type;
        public Mesh wallMesh;
    }

    public enum WallType
    {
        Concrete,
        Portal,
    }
    
    public enum Direction
    {
        Up = 0,
        Right,
        Down,
        Left
    }

    public class OnLevelStart
    {
        public int levelIndex;
        public int target;

        public OnLevelStart(int index, int count)
        {
            levelIndex = index;
            target = count;
        }
    }

    public class OnDirectionUpdated
    {
        public Direction direction;

        public OnDirectionUpdated(Direction direction)
        {
            this.direction = direction;
        }
    }

    public class OnAppleGathered
    {
        
    }

    public class OnDirectionMirrored
    {
        public float duration;

        public OnDirectionMirrored(float duration)
        {
            this.duration = duration;
        }
    }
}