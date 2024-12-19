using System;
using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
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
}
