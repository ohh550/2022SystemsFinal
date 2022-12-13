using UnityEngine;

namespace ReganAlchemy
{
    public class WorldGrid
    {
        public GridObject[,] gridObjects;
        readonly int _gridsize;

        public WorldGrid(int gridSize)
        {
            OccupyGrid(gridSize);
            _gridsize = gridSize;
        }

        public bool IsOnGrid(Vector2Int position)
        {
            bool inXRange = position.x > 0 && position.x < _gridsize;
            bool inYRange = position.y > 0 && position.y < _gridsize;
            return inXRange && inYRange;
        }

        private void OccupyGrid(int gridSize)
        {
            gridObjects = new GridObject[gridSize, gridSize];
        }
    }
}
