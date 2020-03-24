using UnityEngine;

namespace Assets._Scripts.ExtensionMethods
{
    public static class VectorExtension
    {
        public static int ToIndex(this Vector2 vec, int gridWidth, Vector2 cellSize)
        {
            return (int)(vec.x / cellSize.x) + (int)(vec.y / cellSize.y) * gridWidth;
        }
    }
}
