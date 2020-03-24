using UnityEngine;

namespace Assets._Scripts.ExtensionMethods
{
    public static class IntegerExtension
    {
        public static Vector2 ToVector2(this int index, int gridWidth, Vector2 cellSize)
        {
            return new Vector2((int)(index % gridWidth * cellSize.x) + cellSize.x / 2, (int)(index / gridWidth * cellSize.y) + cellSize.y / 2);
        }
    }
}
