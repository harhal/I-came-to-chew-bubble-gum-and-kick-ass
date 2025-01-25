using BubbleGumGuy;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
    public class GridMovement : MonoBehaviour
    {
        public enum GridDirection
        {
            Up,
            Down,
            Right,
            Left
        }
        
        private Grid _theGrid;
        private Tilemap _tilemap;
        
        [SerializeField]
        private Vector2Int startGridPosition;
        
        [SerializeField]
        private Vector2 cellOffset;

        private void Start()
        {
            _theGrid = The.Grid.GetComponent<Grid>();
            _tilemap = The.Grid.GetComponentInChildren<Tilemap>();
            MoveTo(startGridPosition);
        }

        private Vector2Int _gridPosition;

        public bool MoveTo(Vector2Int position)
        {
            if (!GridOccupation.IsFree(position))
            {
                return false;
            }
            
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            TileBase tile = _tilemap.GetTile(gridPositionV3);
            TileData tileData = default;
            tile.GetTileData(gridPositionV3, _tilemap, ref tileData);
            if (tileData.colliderType == Tile.ColliderType.Grid)
            {
                return false;
            }
            
            if (!GridOccupation.IsFree(_gridPosition))
            {
                GridOccupation.Free(_gridPosition);
            }
            
            var worldLocation = _theGrid.GetCellCenterWorld(gridPositionV3) + 
                                new Vector3(cellOffset.x, cellOffset.y, 0);
            transform.position = worldLocation;
            _gridPosition = position;
            GridOccupation.Occupy(_gridPosition, gameObject);
            
            return true;
        }

        public void Go(GridDirection direction, int distance = 1)
        {
            Vector2Int destLocation;
            
            switch (direction)
            {
                case GridDirection.Up: 
                    destLocation = new Vector2Int(_gridPosition.x, _gridPosition.y + distance);
                    break;
                case GridDirection.Down:
                    destLocation = new Vector2Int(_gridPosition.x, _gridPosition.y - distance);
                    break;
                case GridDirection.Right:
                    destLocation = new Vector2Int(_gridPosition.x + distance, _gridPosition.y);
                    break;
                case GridDirection.Left:
                    destLocation = new Vector2Int(_gridPosition.x - distance, _gridPosition.y);
                    break;
                default:
                    //Debug.LogError("Invalid direction");
                    return;
            }
            
            MoveTo(destLocation);
        }
    }
}
