using UnityEngine;

namespace Core
{
    public class BaseGridPlacable: MonoBehaviour
    {
        [SerializeField]
        public Vector2 cellOffset;

        [SerializeField]
        public Vector2Int gridPosition;

        public void SetGridPosition(Vector2Int position, Grid grid)
        {
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            var worldLocation = grid.GetCellCenterWorld(gridPositionV3) + 
                                new Vector3(cellOffset.x, cellOffset.y, 0);
            transform.position = worldLocation;
            gridPosition = position;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }
    }
}