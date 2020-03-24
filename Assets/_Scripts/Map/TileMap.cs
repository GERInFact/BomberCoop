using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets._Scripts.ExtensionMethods;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Scripts.Map
{
    public class TileMap : MonoBehaviour
    {

        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private Vector2 _gridDimension;
        [SerializeField] private Vector2 _cellSize;

        public Vector2 CellSize => this._cellSize;
        public Vector2 GridDimension => this._gridDimension;
        public Dictionary<string, List<GameObject>> Tiles { get; } = new Dictionary<string, List<GameObject>>();

        public List<GameObject> this[[NotNull] string tagName] => this.Tiles[tagName];

        public void Create()
        {
            this.Tiles.Clear();

            var cellCount = this.GridDimension.x * this.GridDimension.y;
            if (cellCount < 9) return;

            for (var i = 0; i < cellCount; i++)
                this.AddElement(i, this.IsWall(i, cellCount) ? this._wallPrefab : this._tilePrefab);
        }

        private bool IsWall(int cellIndex, float cellCount)
        {
            return this.IsBottomWall(cellIndex) || this.IsLeftWall(cellIndex) || this.IsRightWall(cellIndex) ||
                   this.IsTopWall(cellIndex, cellCount);
        }

        private bool IsTopWall(int cellIndex, float cellCount)
        {
            return cellIndex > cellCount - this.GridDimension.x && cellIndex < cellCount - 1;
        }

        private bool IsRightWall(int cellIndex)
        {
            return (cellIndex + 1) / this.GridDimension.x % 1 == 0;
        }

        private bool IsLeftWall(int cellIndex)
        {
            return cellIndex % ((int)this.GridDimension.x) == 0;
        }

        private bool IsBottomWall(int cellIndex)
        {
            return cellIndex > 0 && cellIndex < this.GridDimension.x - 1;
        }

        private void AddElement(int cellIndex, [NotNull] GameObject tilePrefab)
        {
            var position2D = cellIndex.ToVector2((int)this.GridDimension.x, this.CellSize);
            var tile = Instantiate(tilePrefab, new Vector3(position2D.x + this.transform.position.x, 0, position2D.y + this.transform.position.y),
                this.transform.rotation);
            this.AttachAsChild(tile);
            if (!this.Tiles.ContainsKey(tilePrefab.tag))
                this.Tiles.Add(tilePrefab.tag, new List<GameObject>() { tilePrefab });
            else
                this.Tiles[tilePrefab.tag].Add(tile);
        }

        private void AttachAsChild(GameObject tile)
        {
            tile.transform.parent = this.transform;
            tile.transform.localPosition = new Vector3(tile.transform.position.x, 0, tile.transform.position.z);
        }
    }
}
