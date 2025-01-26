using System;
using BubbleGumGuy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
    public class GridMovement : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Direction = Animator.StringToHash("Direction");

        public enum GridDirection
        {
            East,
            South,
            North,
            West
        }
        
        private Grid _theGrid;
        private Tilemap _tilemap;
        private Animator _animator;
        private Vector2Int _deferredDestination;
        
        [SerializeField]
        private Vector2Int startGridPosition;
        
        [SerializeField]
        private Vector2 cellOffset;

        private Vector2Int _gridPosition;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _theGrid = The.Grid.GetComponent<Grid>();
            _tilemap = The.Grid.GetComponentInChildren<Tilemap>();
            MoveTo(startGridPosition);
        }
        
        public Vector2Int DirectionToLocation(GridDirection direction, int distance = 1)
        {
            switch (direction)
            {
                case GridDirection.North: 
                    return new Vector2Int(_gridPosition.x, _gridPosition.y + distance);
                case GridDirection.South:
                    return  new Vector2Int(_gridPosition.x, _gridPosition.y - distance);
                case GridDirection.East:
                    return  new Vector2Int(_gridPosition.x + distance, _gridPosition.y);
                case GridDirection.West:
                    return  new Vector2Int(_gridPosition.x - distance, _gridPosition.y);
                default:
                    return new Vector2Int();
            }
        }

        bool CanMoveTo(Vector2Int position)
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
            
            return true;
        }

        public bool MoveTo(Vector2Int position)
        {
            if (!CanMoveTo(position))
            {
                return false;
            }
            
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            var worldLocation = _theGrid.GetCellCenterWorld(gridPositionV3) + 
                                new Vector3(cellOffset.x, cellOffset.y, 0);
            transform.position = worldLocation;
            
            if (!GridOccupation.IsFree(_gridPosition))
            {
                GridOccupation.Free(_gridPosition);
            }
            
            _gridPosition = position;
            
            GridOccupation.Occupy(_gridPosition, gameObject);
            
            return true;
        }

        public bool DeferredGo(GridDirection direction, int distance = 1)
        {
            var destLocation = DirectionToLocation(direction, distance);
            
            if (!CanMoveTo(destLocation))
            {
                return false;
            }

            _deferredDestination = destLocation;
            
            if (_animator)
            {
                _animator.SetInteger(Direction, (int)direction);
                _animator.SetTrigger(Move);
            }

            return true;
        }

        public void ReleasedDeferredMove()
        {
            MoveTo(_deferredDestination);
        }
    }
}
