using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
    public class GridOccupation : MonoBehaviour
    {
        private static GameObject[,] _occupationGrid;
        
        [SerializeField]
        private Vector2Int gridSize;
        
        [SerializeField]
        private Tilemap tilemap;
    
        private void Awake()
        {
            _occupationGrid = new GameObject[gridSize.x, gridSize.y];
        }

        private static bool IsValidLocation(Vector2Int location)
        {
            return (location.x >= 0 && location.x < _occupationGrid.GetLength(0) &&
                    location.y >= 0 && location.y < _occupationGrid.GetLength(1));
        }

        public static GameObject GetOccupation(Vector2Int location)
        {
            return IsValidLocation(location) ? _occupationGrid[location.x, location.y] : null;
        }

        public static bool IsFree(Vector2Int location)
        {
            return IsValidLocation(location) && !GetOccupation(location);
        }

        public static void Occupy(Vector2Int location, GameObject occupation)
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

        public static void Free(Vector2Int location)
        {
            _occupationGrid[location.x, location.y] = null;
        }
    }
}
