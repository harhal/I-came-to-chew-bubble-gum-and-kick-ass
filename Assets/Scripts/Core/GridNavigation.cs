using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
    public class GridNavigation : MonoBehaviour
    {
        private GameObject[,] _occupationGrid;
        
        [SerializeField]
        private Vector2Int gridSize;
        
        [SerializeField]
        private Tilemap tilemap;
    
        public Vector2Int GetGridSize()
        {
            return gridSize;
        }
    
        private void Awake()
        {
            _occupationGrid = new GameObject[gridSize.x, gridSize.y];
        }

        public bool IsValidLocation(Vector2Int location)
        {
            return (location.x >= 0 && location.x < _occupationGrid.GetLength(0) &&
                    location.y >= 0 && location.y < _occupationGrid.GetLength(1));
        }

        public GameObject GetOccupation(Vector2Int location)
        {
            return IsValidLocation(location) ? _occupationGrid[location.x, location.y] : null;
        }

        public bool IsFree(Vector2Int location, bool bIgnoreObstacles = false, bool bIgnoreOccupations = false)
        {
            return IsValidLocation(location) && (bIgnoreObstacles || IsObstacleFree(location)) && (bIgnoreOccupations || !GetOccupation(location));
        }

        public void Occupy(Vector2Int location, GameObject occupation)
        {
            if (!IsValidLocation(location))
            {
                return;
            }

            if (!_occupationGrid[location.x, location.y])
            {
                _occupationGrid[location.x, location.y] = occupation;
            }
        }

        public void Free(Vector2Int location)
        {
            _occupationGrid[location.x, location.y] = null;
        }

        public bool IsObstacleFree(Vector2Int position)
        {
            if (!IsValidLocation(position))
            {
                return false;
            }
            
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            TileBase tile = tilemap.GetTile(gridPositionV3);
            if (!tile)
            {
                return false;
            }
                
            TileData tileData = default;
            tile.GetTileData(gridPositionV3, tilemap, ref tileData);
            if (tileData.colliderType == Tile.ColliderType.Grid)
            {
                return false;
            }
            
            return true;
        }
    }
}
