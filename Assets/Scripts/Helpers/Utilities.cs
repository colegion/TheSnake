using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        [SerializeField] private List<WallConfig> wallConfigs;
        
        public const string LightTilePath = "Prefabs/GrassLight";
        public const string DarkTilePath = "Prefabs/GrassDark";

        public const int BlockLayer = 0;
        public const int FreeLayer = 1;

        private static List<WallConfig> _wallConfigs;

        private void Start()
        {
            _wallConfigs = wallConfigs;
        }

        public static Mesh GetWallMeshByType(WallType type)
        {
            return _wallConfigs.Find(c => c.type == type).wallMesh;
        }
        

        public static bool IsEdgeCell(int yCoord) => yCoord == 0;
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
}
