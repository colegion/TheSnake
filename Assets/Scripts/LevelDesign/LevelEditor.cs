using System.Drawing;
using UnityEngine;
using Size = Helpers.Size;

namespace LevelDesign
{
    public class LevelEditor : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;

        [HideInInspector] public int width;
        [HideInInspector] public int height;
        
        public void CreateGrid()
        {
            var size = new Size(width, height);
            levelGenerator.GenerateGrid(size);
        }
    }
}
