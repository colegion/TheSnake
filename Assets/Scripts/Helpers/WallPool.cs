using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class WallPool : MonoBehaviour
    {
        [SerializeField] private Transform poolParent;
        [SerializeField] private int poolAmount;

        private static WallPool _instance;

        public static WallPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("WallPool").AddComponent<WallPool>();
                }

                return _instance;
            }
        }
        private static List<Wall> _pooledWalls;
        private void Start()
        {
            PoolWalls();
        }

        private void PoolWalls()
        {
            _pooledWalls = new List<Wall>();
            var prefab = Resources.Load<BaseTile>(Utilities.WallPath);
            for (int i = 0; i < poolAmount; i++)
            {
                var wall = Instantiate(prefab, Vector3.zero, Quaternion.identity, poolParent);
                _pooledWalls.Add((Wall)wall);
            }
        }

        public Wall GetAvailableWall()
        {
            foreach (var wall in _pooledWalls)
            {
                if (!wall.gameObject.activeSelf)
                {
                    return wall;
                }
            }
            
            var prefab = Resources.Load<BaseTile>(Utilities.WallPath);
            var temp = Instantiate(prefab, Vector3.zero, Quaternion.identity, poolParent);
            _pooledWalls.Add((Wall)temp);
            return (Wall)temp;
        }
    }
}
