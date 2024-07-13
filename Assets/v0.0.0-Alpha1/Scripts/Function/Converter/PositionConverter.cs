using UnityEngine;

namespace Function.PositionConverter
{
    public class PositionConverter : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private Grid grid;

        public Vector3Int WorldToGridPosition(Vector3 position)
        {
            return this.grid.WorldToCell(position + new Vector3(2.5f, 0, 2.5f));
        }

        public Vector3 GridPositionToWorld(Vector3Int gridPosition)
        {
            return this.grid.CellToWorld(gridPosition);
        }
    }
}